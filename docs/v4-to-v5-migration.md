# Migrating from v4.x (or earlier) to v5.x

There were a handful of breaking changes introduced in v5.x of the library. These include:

- `MountebankClient` methods are now async by default and have been renamed to include an `Async` suffix
- The `MountebankClient` constructor which overrides the Mountebank URL now takes a `Uri` object instead of a `string`
- The `CreateHttpImposter`, `CreateHttpsImposter`, and `CreateTcpImposter` methods on `MountebankClient` have been updated to actually create imposters in Mountebank rather than act as simple factory methods for the imposter objects
- The `Submit` methods on `MountebankClient` have been removed since the imposter creation methods now handle submission to Mountebank
- Imposter configuration (ex. `RecordRequests`) is now exposed via properties on the imposters themselves rather than through arguments to the imposter creation methods
- The `Behavior` class has been renamed to `WaitBehavior` and a constructor was added for setting the latency value
- Responses now require an array of behaviors instead of a single one
- The `Headers` and `QueryParameters` properties on `HttpPredicateFields` now have a type of `IDictionary<string, object>` instead of `IDictionary<string, string>` to support specifying arrays of values
- The `Headers` property on `HttpResponseFields` now has a type of `IDictionary<string, object>` instead of `IDictionary<string, string>` to support returning arrays of values
- Various collection types in the models have been changed. See [#119](https://github.com/mattherman/MbDotNet/pull/119) for details.
- The following types have been renamed: `StubBase` -> `Stub`, `PredicateBase` -> `Predicate`, `ResponseBase` -> `Response`
  - This should not impact most users since these are abstract base classes
- If a `Key` or `Cert` are provided when creating an HTTPS imposter they will now be validated and an exception will be thrown if they are not valid PEM-formatted strings
- The `MbDotNet.Enums` namespace has been removed and its types moved elsewhere
- `IHttpClientWrapper` / `HttpClientWrapper` are no longer `public`

Continue reading for detailed guidance on the updates you may need to make in order to upgrade to v5.x of the package.

## Async Client Methods

All client methods are now `async` and their names include an `Async` suffix. To migrate you should update the method names where necessary and `await` them.

_v4_

```
client.GetHttpImposter(4545);
```

_v5_

```
await client.GetHttpImposterAsync(4545);
```

This will require the code where the methods are called to be async as well. For test methods that should just involve changing the signature.

_v4_

```
public void MyTest() { ... }
```

_v5_

```
public async Task MyTest { ... }
```

## Imposter Configuration and Creation

This is by far the largest change between the two versions. Previously the imposter creation methods were simple factory methods on the client that assisted with the creation and configuration of imposter objects. In order to actually create the imposters in Mountebank users would need to call the `Submit` method.

The factory methods have always been a little confusing for users since they include the word "Create" in them. Many users initially think those methods will create the imposter in Mountebank itself.

In order to make this more clear, those methods have been modified to handle the creation and configuration of the imposter instance as well as its submission to Mountebank in a single method call.

There are two main styles by which you create and configure imposters in v5.x. The first (and recommended) style uses a callback for configuration while the second style allows you to create and configure the imposter instance yourself.

_v4_

```
var imposter = client.CreateHttpImposter(4545, "MyImposter", true);
imposter.AddStub()
	.OnPathAndMethodEqual("/books", Method.Get)
	.ReturnsJson(HttpStatusCode.OK, books);
client.Submit(imposter);
```

_v5_

```
# First style (recommended)
var firstStyle = await client.CreateHttpImposter(4545, "MyImposter", imposter =>
{
	imposter.RecordRequests = true;

	imposter.AddStub()
		.OnPathAndMethodEqual("/books", Method.Get)
		.ReturnsJson(HttpStatusCode.OK, books);
});

# Second style
var secondStyle = new HttpImposter(4545, "MyImposter", new HttpImposterOptions { RecordRequests = true });
await client.CreateHttpImposter(secondStyle);
```

The first style is recommended over the second since it encapsulates the configuration and creation of the imposter. With the second style it is easier for users to accidentally modify their imposter object after it has been created in Mountebank which will not be reflected. If all of your imposter configuration is performed via the callbacks instead, you are more likely to avoid that mistake.

### Removal of `Submit` Methods

With these changes the `Submit` methods were no longer necessary so they have been removed.

### Imposter Configuration

As shown above, imposter configuration is now done through imposter properties or constructors rather than the client factory methods.

_v4_

```
var imposter = client.CreateHttpImposter(4545, "MyImposter", recordRequests: true, defaultResponse: response, allowCors: true);
```

_v5_

```
# Via properties
var imposter = new HttpImposter(4545, "MyImposter");
imposter.RecordRequests = true;
imposter.DefaultResponse = response;
imposter.AllowCORS = true;

# Via constructor configuration object
var imposter = new HttpImposter(4545, "MyImposter", new HttpImposterConfiguration
{
	RecordRequests = true,
	DefaultResponse = response,
	AllowCORS = true
});
```

## `Behavior` Renamed to `WaitBehavior`

The `Behavior` class has been renamed to `WaitBehavior` to support adding additional behavior types in the future. A constructor has also been added which requires the `latencyInMilliseconds` to be provided when the class is instantiated.

_v4_

```
var behavior = new Behavior { LatencyInMilliseconds = 1000 };
```

_v5_

```
var behavior = new WaitBehavior(1000);
```

## Response Behavior Array

Responses now accept an array of behaviors instead of a single behavior. The JSON property for behaviors has also been updated from `_behaviors` to `behaviors` to match the current Mountebank contracts.

This resulted in changes to the constructor signatures of `IsResponse` and `ProxyResponse`.

_v4_

```
var response = new IsResponse(fields, new WaitBehavior(1000));
```

_v5_

```
var response = new IsResponse(fields, new []{ new WaitBehavior(1000) });
```

## Updates to `Headers` and `QueryParameters` Types

The `Headers` and `QueryParameters` properties on `HttpPredicateFields` and the `Headers` property on `HttpResponseFields` have had their type updated from `IDictionary<string, string>` to `IDictionary<string, object>`. This was done to support setting the keys in those collections to arrays of values instead of a single string.

This change should not require you to change the things you are storing in those collections at all, but you will need to change the types you are instantiating when you construct those collections.

_v4_

```
var fields = new HttpPredicateFields
{
	QueryParameters = new Dictionary<string, string> { ["q"] = "search" }
};
```

_v5_

```
var fields = new HttpPredicateFields
{
	QueryParameters = new Dictionary<string, object> { ["q"] = "search" }
};
```

## `Key` and `Cert` Validation

The `Key` and `Cert` properties of HTTPS imposters now need to be valid PEM-formatted strings. Previously you could create the imposters with invalid values but Mountebank would reject them as soon as you submitted the imposter.

_v4_

```
var imposter = client.CreateHttpsImposter(4545, "MyImposter", key: "invalid_key", cert: "invalid_cert");
```

_v5_

```
var imposter = new HttpsImposter(4545, "MyImposter", new HttpsImposterConfiguration
{
	Key = "-----BEGIN CERTIFICATE-----base64_encoded_data-----END CERTIFICATE-----",
	Cert = "-----BEGIN CERTIFICATE-----base64_encoded_data-----END CERTIFICATE-----"
});
```

## Removal of `MbDotNet.Enums` namespace

The `MbDotNet.Enums` namespace has been removed since it did not make sense to group those types by what they are vs. what they represent.

The following types have been moved to the specified namespaces:

- `MbDotNet.Enums.Method` -> `MbDotNet.Models.Method`
- `MbDotNet.Enums.ProxyMode` -> `MbDotNet.Models.ProxyMode`
- `MbDotNet.Enums.Protocol` -> `MbDotNet.Models.Imposters.Protocol`
- `MbDotNet.Enums.TcpMode` -> `MbDotNet.Models.Imposters.TcpMode`

Any `using` statements for `MbDotNet.Enums` will need to be updated to reflect the new locations.
