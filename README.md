[![Build status](https://ci.appveyor.com/api/projects/status/q5rn71ncmimgg3y3?svg=true)](https://ci.appveyor.com/project/mattherman/mbdotnet)

# MbDotNet
A .NET client library for interacting with mountebank (www.mbtest.org). This project aims to reduce the amount of mountebank knowledge required in order to create usable stubs.

A simple example:
```
var imposter = _client.CreateHttpImposter(4545, "My Imposter");
imposter.AddStub()
  .OnPathAndMethodEqual("/customers/123", Method.Get)
  .ReturnsXml(HttpStatusCode.OK, new Customer { Email = "customer@test.com" });
```

To get started, see [Usage Examples (v3)](https://github.com/mattherman/MbDotNet/wiki/Usage-Examples-%28v3%29).

## NuGet Package ##

The library is available for install as a NuGet package.

https://www.nuget.org/packages/MbDotNet

## Unsupported Functionality ##

The following Mountebank functionality is not yet supported:
- SMTP imposters
- Stub behaviors
- All response types other than "is"
- The "exists", "or", "and", and "inject" predicates

Pull requests are always welcome.
