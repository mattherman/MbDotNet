# Introduction

MbDotNet is a .NET client library for the [Mountebank](https://mbtest.org) testing tool created by Brandon Byars. Mountebank provides cross-platform, multi-protocol test doubles over the wire that enable you to test distributed applications by mocking and stubbing your applications dependencies at the network level.

Test doubles in Mountebank are called _imposters_. An imposter is configured to respond to requests made to a specific port. Each imposter defines one or more _stubs_ that control how the _imposter_ responds to those requests. A single _stub_ defines _predicates_ which specify whether that stub should be matched by a request. If matched, the stub also defines _responses_ which specify the actual response that should be returned.

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
