# MbDotNet
A .NET client library for interacting with mountebank (www.mbtest.org).

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
