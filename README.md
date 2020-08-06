![](https://github.com/mattherman/mbdotnet/workflows/CI/badge.svg)

# MbDotNet

A .NET client library for interacting with mountebank (www.mbtest.org). This project aims to reduce the amount of mountebank knowledge required in order to create usable stubs.

A simple example:
```
var imposter = _client.CreateHttpImposter(4545, "My Imposter");
imposter.AddStub()
  .OnPathAndMethodEqual("/customers/123", Method.Get)
  .ReturnsXml(HttpStatusCode.OK, new Customer { Email = "customer@test.com" });
```

The project currently targets .NET Standard 1.3, which is compatible with .NET Framework 4.6. If you need to use it in a project targeting an older framework version, such as .NET Framework 4.5, please use version 3.X.X of the package.

To get started, see [Usage Examples (v4)](https://github.com/mattherman/MbDotNet/wiki/Usage-Examples-%28v4%29).

## NuGet Package

The library is available for install as a NuGet package.

https://www.nuget.org/packages/MbDotNet

## Unsupported Functionality

The following Mountebank functionality is not yet supported:
- SMTP imposters
- Stub behaviors
- The "inject" response
- The "exists" and "inject" predicates

Pull requests are always welcome.

## Development

### Prerequisites

The following items are necessary in order to build/test the project:
* .NET Core SDK 2.0
* .NET Core Runtime 2.0
* Mountebank

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
against an actual mountebank instance. In order for those tests to succeed, mountebank 
will need to be run with the `--mock --debug` option provided. See http://www.mbtest.org/docs/api/overview#get-imposter.

If you would prefer to run mountebank via docker, please execute the following command from the root directory:
```docker-compose up```

If you would like to just run the unit tests (which do not require mountebank), run the following:
```
dotnet test --filter TestCategory=Unit
```

Similarly, you can filter to only the acceptance tests using `--filter TestCategory=Acceptance`.
