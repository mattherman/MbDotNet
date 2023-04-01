# Introduction

MbDotNet is a .NET client library for the [Mountebank](https://mbtest.org) testing tool created by Brandon Byars. Mountebank provides cross-platform, multi-protocol test doubles over the wire that enable you to test distributed applications by mocking and stubbing your applications dependencies at the network level.

Test doubles in Mountebank are called _imposters_. An imposter is configured to respond to requests made to a specific port. Each imposter defines one or more _stubs_ that control how the imposter responds to those requests. A single stub defines _predicates_ which specify whether that stub should be matched by a request. If matched, the stub also defines _responses_ which specify the actual response that should be returned.

Here is an example HTTP imposter that is configured on port 4545 and has two stubs:

```
{
    "port": 4545,
    "protocol": "http",
    "stubs": [
        {
            predicates: [
                {
                    "matches": {
                        "method": "GET",
                        "path": "/books"
                    }
                }
            ],
            responses: [
                {
                    "is": {
                        "statusCode": 200,
                        "body": [
                            { "id": 1, "name": "Great Expectations" },
                            { "id": 2, "name": "A Christmas Carol" }
                        ]
                    }
                }
            ]
        },
        {
            "responses": [
                {
                    "is": {
                        "statusCode": 404
                    }
                }
            ]
        }
    ]
}
```

When a request is made to the imposter it will attempt to find a matching stub in the order they are defined. In this example the first stub is configured to match any requests that have a method of "GET" and a path of "/books". If the request matches that predicate, a response with a status code of 200 and the specified JSON body will be returned. If that stub is not matched, the second stub will be checked and matched automatically since there are no predicates defined, causing it to return a status code of 404.

Most interaction with Mountebank is performed through its API. For example, to create the imposter above you would `POST` that JSON object to `http://localhost:2525/imposters` (`2525` is the default port). You could then retrieve information about that imposter through a `GET` to `http://localhost:2525/imposters/4545`.

MbDotNet is an abstraction layer over this API that allows you to easily configure your imposters using plain .NET types rather than using the JSON format directly. It also provides methods for most of the API operations that Mountebank exposes.

If you're unfamiliar with Mountebank, I suggest reading through that [project's documentation](https://mbtest.org) before starting to use MbDotNet.

# Getting Started with MbDotNet

One of the primary goals of this project is to simplify the creation of imposters using natural language where possible. For example, creating the imposter from the previous section would look like this:

```
var client = new MountebankClient();
await client.CreateHttpImposterAsync(4545, imposter =>
{
	var books = new []
	{
		new Book { Id = 1, Name = "Great Expectations" },
		new Book { Id = 2, Name = "A Christmas Carol" }
	};

	imposter.AddStub()
		.OnPathAndMethodEqual("/books", Method.Get)
		.ReturnsJson(HttpStatusCode.OK, books);

	imposter.AddStub()
		.ReturnsStatus(HttpstatusCode.NotFound);
});
```

First we create a new `MountebankClient` which we use to interact with Mountebank. Then we create a new HTTP imposter on port 4545 using the `CreateHttpImposterAsync` method. This method accepts a configurator callback which we can use to configure the imposter before creation.

The imposter configuration uses a fluent interface that exposes helper functions like `OnPathAndMethodEqual` and `ReturnsJson` that make defining your stubs more natural.

Methods like this exist for the most common predicates and responses, but on occasion you may need more control. You can add more complex predicates and responses to your stubs using the `On` and `Returns` methods:

```
var adminPath = new StartsWithPredicate<HttpPredicateFields>(
{
	Path = "/admin"
});

imposter.AddStub()
	.On(adminPath)
	.Returns(
		HttpStatusCode.Forbidden,
		new Dictionary<string, object> { ["Date"] = DateTime.UtcNow },
		"User does not have admin rights"
	);
```

In this example we are adding a stub that will match any requests where the path starts with "/admin" and returning a 403 response with a custom body and specific headers. Since the stub does not expose a helper for the "startsWith" predicate we needed to craft the predicate ourselves and use the `On` method to add it to the stub. We then use the `Returns` method to have some more control over each part of the response.

We can also retrieve imposters and verify the requests that have been made to them for mock verification. A full test with imposter setup and verification might look something like this:

```
var client = new MountebankClient();

var books = new []
{
	new Book { Id = 1, Name = "Great Expectations" },
	new Book { Id = 2, Name = "A Christmas Carol" }
};

await client.CreateHttpImposterAsync(4545, imposter =>
{
	imposter.RecordRequests = true;

	imposter.AddStub()
		.OnPathAndMethodEqual("/books", Method.Get)
		.ReturnsJson(HttpStatusCode.OK, books);
});

var codeUnderTest = new CodeUnderTest(apiUrl: "http://localhost:4545");
var result = await codeUnderTest.GetBooks();

Assert.Equal(result, books);

var booksImposter = await client.GetHttpImposterAsync(4545);

Assert.Equal(1, booksImposter.NumberOfRequests);
Assert.Equal("GET", booksImposter.Requests[0].Method);
Assert.Equal("/books", booksImposter.Requests[0].Path);
```

For this test we setup a single stub and enable Mountebank's request recording functionality for the imposter using `imposter.RecordRequests = true`. We then exercise the code under test which relies on the API that we are mocking and verify the result. Finally, we retrieve the imposter and inspect the `Requests` collection to verify the behavior we expected.

The official [documentation](https://mbtest.org) has many more examples of imposter creation with all of the different [predicates](http://www.mbtest.org/docs/api/predicates) and [responses](http://www.mbtest.org/docs/api/stubs) that are available. Most of these examples have a corresponding test in the [DocumentationTests.cs](https://github.com/mattherman/MbDotNet/blob/master/MbDotNet.Tests/Acceptance/DocumentationTests.cs) file that may help you translate between the JSON imposter definitions and how they would be defined with MbDotNet. If you are struggling to define a specific predicate or response I would suggest looking there first.

# Deep Dive

## Interacting with Mountebank

All interaction with Mountebank is done through the `MountebankClient` class. This class exposes the various [imposter operations](http://www.mbtest.org/docs/api/overview) as well as more diagnostic operations like viewing logs or configuration information.

The default constructor will assume that Mountebank is running at `http://localhost:2525`. There is an alternative constructor that accepts a URI if you need to override this value.

The following methods are used to create HTTP imposters:

```
CreateHttpImposterAsync(int? port, string name, Action<HttpImposter> imposterConfigurator);
CreateHttpImposterAsync(int? port, Action<HttpImposter> imposterConfigurator)
CreateHttpImposterAsync(HttpImposter imposter)
# Additional methods for creating HTTPS, TCP, and SMTP imposters...
```

There are similar methods for HTTPS, TCP, and SMTP imposters as well. I suggest using one of the first two overloads which take a callback for configuring the imposter. This encapsulates the configuration and avoids situations where you might modify your imposter object after it has been submitted to Mountebank which would likely cause confusion since the changes would not be represented there. If you'd like to create the imposter and configure it separately you can still use the last overload to submit it to Mountebank.

The client also exposes methods to retrieve imposters:

```
GetHttpImposterAsync(int port)
GetHttpsImposterAsync(int port)
GetTcpImposterAsync(int port)
GetSmtpImposterAsync(int port)
GetImpostersAsync()
```

These methods return a simplified representation of the imposter which does not include the configured stubs, but does have requests (if `RecordRequests` is true) and stub matches (if Mountebank is run with the `--debug` flag).

The client allows you to delete imposters or clear saved requests:

```
DeleteImposterAsync(int port)
DeleteAllImpostersAsync()
DeleteSavedRequestsAsync(int port)
```

It also lets you modify stubs on a specific imposter:

```
ReplaceHttpImposterStubsAsync(int port, IEnumerable<HttpStub> replacementStubs)
ReplaceHttpImposterStubAsync(int port, HttpStub replacementStub, int stubIndex)
AddHttpImposterStubAsync(int port, HttpStub newStub, int? newStubIndex)
RemoveStubAsync(int port, int stubIndex)
```

Finally, there are a handful of diagnostic methods:

```
GetEntryHypermediaAsync()
GetConfigAsync()
GetLogsAsync()
```

## Creating HTTP/HTTPS Stubs

There are a handful of predicate helpers on the `HttpStub` class for common predicate setup as well as the `On(Predicate)` method which lets you configure more complex predicates.

Similarly, there are helpers that allow you to more easily craft your responses. There are also helpers for configuring [proxy responses](http://www.mbtest.org/docs/api/proxies) and a generic `Returns(Response)` method for adding custom responses.

Examples:

```
imposter.AddStub()
	.OnPathEquals("/books")
	.ReturnsJson(HttpStatusCode.OK, books);

imposter.AddStub()
	.OnMethodEquals(Method.Post)
	.ReturnsStatus(HttpStatusCode.Created);

imposter.AddStub()
	.OnPathAndMethodEqual("/books", Method.Get)
	.ReturnsXml(HttpStatusCode.OK, books);

imposter.AddStub()
	.OnInjectedFunction("function(config) { return true; }")
	.ReturnsBody("a=1&b=2");

var predicateFields = new HttpPredicateFields
{
	Path = "/binaryData"
};
var responseFields = new HttpResponseFields
{
	StatusCode = HttpStatusCode.OK,
	ResponseObject = "data",
	Mode = "binary"
}
imposter.AddStub()
	.On(new StartsWithPredicate<HttpPredicateFields>(predicateFields))
	.Returns(new IsResponse<HttpResponseFields>(responseFields));
```

## Creating TCP Stubs

The `TcpStub` class exposes a limited set of helpers as well as a generic `On(Predicate)` and `Returns(Response)` methods.

Examples:

```
imposter.AddStub()
	.OnDataEquals("123456")
	.ReturnsData("abcdefg");

imposter.AddStub()
	.OnInjectedFunction("function(config) { return true; }")
	.ReturnsData("abcdefg");
```

## Predicates

MbDotNet supports all of the predicate types that are supported by Mountebank (as of v2.8.2).

| Mountebank Predicate Type                                                       | MbDotNet Class        |
| ------------------------------------------------------------------------------- | --------------------- |
| [`equals`](http://www.mbtest.org/docs/api/predicates#predicates-equals)         | `EqualsPredicate`     |
| [`deepEquals`](http://www.mbtest.org/docs/api/predicates#predicates-deepEquals) | `DeepEqualsPredicate` |
| [`contains`](http://www.mbtest.org/docs/api/predicates#predicates-contains)     | `ContainsPredicate`   |
| [`startsWith`](http://www.mbtest.org/docs/api/predicates#predicates-startsWith) | `StartsWithPredicate` |
| [`endsWith`](http://www.mbtest.org/docs/api/predicates#predicates-endsWith)     | `EndsWithPredicate`   |
| [`matches`](http://www.mbtest.org/docs/api/predicates#predicates-matches)       | `MatchesPredicate`    |
| [`exists`](http://www.mbtest.org/docs/api/predicates#predicates-exists)         | `ExistsPredicate`     |
| [`not`](http://www.mbtest.org/docs/api/predicates#predicates-not)               | `NotPredicate`        |
| [`or`](http://www.mbtest.org/docs/api/predicates#predicates-or)                 | `OrPredicate`         |
| [`and`](http://www.mbtest.org/docs/api/predicates#predicates-and)               | `AndPredicate`        |
| [`inject`](http://www.mbtest.org/docs/api/predicates#predicates-inject)         | `InjectPredicate`     |

Most predicates expect a set of request fields, like `HttpPredicateFields`, with specific values to match in some way. However, some predicates like `exists` expect boolean values for those request fields. In those cases you should use `HttpBooleanPredicateFields`.

The `inject` predicate type can be used to inject a custom Javascript function that will determine whether or not to match a request.

### Multiple Predicates

If a single stub contains multiple predicates, the request needs to match ALL predicates in order for the stub to be matched.

For example, the following stubs are equivalent:

```
var combinedPredicateFields = new HttpPredicateFields
{
	Path = "/books"
	Method = Method.Post
};
imposter.AddStub()
	.On(combinedPredicateFields)
	.ReturnsStatus(HttpStatusCode.Created);

var pathPredicateFields = new HttpPredicateFields { Path = "/books" };
var methodPredicateFields = new HttpPredicateFields { Method = Method.Post };
imposter.AddStub()
	.On(pathPredicateFields)
	.On(methodPredicateFields)
	.ReturnsStatus(HttpStatusCode.Created);
```

## Responses

MbDotNet supports most of the response types that are supported by Mountebank (as of v2.8.2).

| Mountebank Response Type                             | MbDotnet Class  |
| ---------------------------------------------------- | --------------- |
| [`is`](http://www.mbtest.org/docs/api/stubs)         | `IsResponse`    |
| [`proxy`](http://www.mbtest.org/docs/api/proxies)    | `ProxyResponse` |
| [`inject`](http://www.mbtest.org/docs/api/injection) | Not Supported   |
| [`fault`](http://www.mbtest.org/docs/api/faults)     | Not supported   |

The most common response is the `is` response type which lets you specify the exact values that should be returned for each response field.

The `proxy` response type allows you to record and replay behavior by proxying requests to a real service. This requires you to define predicate generators which will determine how Mountebank will create the stubs for the imposter it generates. See the [official documentation](http://www.mbtest.org/docs/api/proxies) for more information about how to use proxies.

Example:

```
var predicateGenerators = new List<MatchesPredicate<HttpBooleanPredicateFields>>
{
	new(new HttpBooleanPredicateFields
	{
		QueryParameters = true
	})
};
imposter.AddStub()
	.ReturnsProxy(
		new Uri("http://origin-server.com",
		ProxyMode.ProxyOnce,
		predicateGenerators)
	);
```

In this example, the first request to the imposter will be proxied to the real service at `http://origin-server.com`. Mountebank will then create a stub based on the predicate generators and the response from the actual service. In this case a predicate that matches on the query parameters of the request will be added. Since the proxy uses `ProxyMode.ProxyOnce` all subsequent requests will be handled by the imposter directly.

### Multiple Responses

If a stub contains multiple responses Mountebank will return each response in a round robin manner when the stub is matched. This can be useful for test scenarios where you expect a different response the second time a request is made, such as a delete scenario:

```
imposter.AddStub()
	.OnPathAndMethodEqual("/books/123", Method.Delete)
	.ReturnsStatus(HttpStatusCode.NoContent)
	.ReturnsStatus(HttpStatusCode.NotFound);
```
