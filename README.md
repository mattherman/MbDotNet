![](https://github.com/mattherman/mbdotnet/workflows/CI/badge.svg)

# MbDotNet

A .NET client library for interacting with [Mountebank](https://www.mbtest.org). This project aims to reduce the amount of Mountebank knowledge required to create and configure imposters through a natural language interface.

A simple example:

```
await _client.CreateHttpImposter(4545, "My Imposter", imposter =>
{
	imposter.AddStub()
		.OnPathAndMethodEqual("/customers/123", Method.Get)
		.ReturnsXml(HttpStatusCode.OK, new Customer { Email = "customer@test.com" });
});
```

To get started, read the documentation [here](https://github.com/mattherman/MbDotNet/blob/master/docs/docs-v5.md).

## NuGet Package

The library is available for install as a NuGet package.

https://www.nuget.org/packages/MbDotNet

The project currently targets .NET Standard 1.3, which is compatible with .NET Framework 4.6. If you need to use it in a project targeting an older framework version, such as .NET Framework 4.5, please use version 3.x of the package.

## Upgrading from v4 (or earlier) to v5

There were a handful of breaking changes in v5 of the library. If you are planning to upgrade to v5, please take a look at the [migration guide](https://github.com/mattherman/MbDotNet/blob/master/docs/v4-to-v5-migration.md).

## Unsupported Functionality

The following Mountebank functionality is not yet supported:

- The [`inject`](http://www.mbtest.org/docs/api/injection) response
- The [`fault`](http://www.mbtest.org/docs/api/faults) response
- Response [behaviors](http://www.mbtest.org/docs/api/behaviors) other than `wait`

Pull requests are always welcome.

## Development

### Prerequisites

The following items are necessary in order to build and test the project:

- .NET SDK 6.0
- Mountebank or Docker Compose

### Building

To build the project, run the following from the root directory:

```
dotnet build
```

### Testing

To run all tests, run the following from the root directory:

```
dotnet test
```

This includes a set of acceptance tests that run
against an actual Mountebank instance. In order for those tests to succeed, Mountebank
will need to be run with the `--allowInjection` and `--debug` options provided. See http://www.mbtest.org/docs/api/overview#get-imposter.

If you would prefer to run Mountebank via docker, please execute the following command from the root directory:
`docker compose up`

If you would like to just run the unit tests (which do not require Mountebank), run the following:

```
dotnet test --filter TestCategory=Unit
```

Similarly, you can filter to only the acceptance tests using `--filter TestCategory=Acceptance`.
