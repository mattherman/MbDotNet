# MbDotNet
A .NET client library for interacting with mountebank (www.mbtest.org).

## NuGet Package ##
The library will be available as a NuGet package at some point in the future.

## Usage Examples
This section (along with the entire README) is a work in progress.

### Creating Imposters ###

This example shows how to create an HTTP imposter on port 8080.

```
var client = new MountebankClient();
var imposter = client.CreateImposter(8080, Protocol.Http);
client.Submit();
```

### Adding Stubs to Imposters ###

This example shows how to add a stub to our imposter that will return a 400 status code.

```
imposter.AddStub().ReturnsStatus(HttpStatusCode.BadRequest);
```

A response object can also be returned from the imposter. The ReturnsJson and ReturnsXml methods will serialize the object into their respective formats and automatically set the Content-Type response header.

```
var obj = new TestObject { Name = "Ten", Value = 10 };
imposter.AddStub().ReturnsJson(HttpStatusCode.OK, obj);
```

If setting up a more complicated response, such as one including headers, the Returns method can be called with a created response object.

```
var headers = new Dictionary<string, string> {{ "Location", "http://localhost:4545/customers/123" }};
var response = new IsResponse(HttpStatusCode.OK, obj, headers);
imposter.AddStub().Returns(response);
```

Predicates can be specified on a stub as well. The following will return a response object if a request is made to the "/test" path.

```
imposter.AddStub().ReturnsJson(HttpStatusCode.OK, obj).OnPathEquals("/test");
```

A specific HTTP method can also be specified on the predicate.

```
imposter.AddStub().ReturnsStatus(HttpStatusCode.MethodNotSupported).OnPathAndMethodEqual("/test", Method.Post);
```

Similar to responses, a more complicated predicate can be added using the On method.

```
var headers = new Dictionary<string, string> {{ "Accept", "text/xml" }};
var queryParameters = new Dictionary<string, string> {{ "id", "10" }};
var predicate = new EqualsPredicate("/test", Method.Get, null, headers, queryParameters);
imposter.AddStub().ReturnsStatus(HttpStatusCode.UnsupportedMediaType).On(predicate);
```

Any number of stubs can be added to an imposter. If there are multiple, the stub with the first set of predicates to match the request will be used by mountebank.

Multiple responses/predicates can also be added to a single stub. If a stub has multiple responses, they will be returned in order. In the following example, the first request will get an 200 status code, but the next will receive a 404 status code.

```
imposter.AddStub().ReturnsStatus(HttpStatus.OK).ReturnsStatus(HttpStatus.NotFound);
```

When a stub has multiple predicates, they will be treated as an AND condition. For example:

```
imposter.AddStub().ReturnsStatus(HttpStatus.OK).OnPathAndMethod("/test", Method.Get);
```

is equivalent to:

```
var firstPredicate = new EqualsPredicate("/test", null, null, null, null);
var secondPredicate = new EqualsPredicate(null, Method.Get, null, null, null);
imposter.AddStub().ReturnsStatus(HttpStatus.OK).On(firstPredicate).On(secondPredicate);
```

### Deleting Imposters ###

All existing imposters can be removed by calling the DeleteAllImposters methods on the client.

```
client.DeleteAllImposters();
```

To remove a single imposter, use the DeleteImposter method.
```
const int port = 8080;
client.DeleteImposter(8080);
```

## Unsupported Functionality ##

Stubs
- Behaviors

Reponses
- All response types other than "is"

Predicates
- All predicate types other than "equals"
- The "caseSensitive", "except", and "xpath" properties
