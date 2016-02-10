# MbDotNet
A .NET client library for interacting with mountebank (www.mbtest.org).

## NuGet Package ##
The library will be available as a NuGet package at some point in the future.

## Usage Examples
This section (along with the entire README) is a work in progress.

This example shows how to create an HTTP imposter on port 8080 that returns a 400 status code.

```
var client = new MountebankClient();
client.CreateImposter(8080, Protocol.Http).Returns(HttpStatusCode.BadRequest).Submit();
```

A response object can also be returned from the imposter.
```
var obj = new { Key = "x", Value = "y" };
client.CreateImposter(8080, Protocol.Http).Returns(HttpStatusCode.OK, obj).Submit();
```

All existing imposters can be removed by calling the DeleteAllImposters methods on the client.
```
client.DeleteAllImposters();
```

To remove a single imposter, use the DeleteImposter method.
```
const int port = 8080;
client.DeleteImposter(8080);
```
