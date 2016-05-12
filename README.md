[![Build status](https://ci.appveyor.com/api/projects/status/q5rn71ncmimgg3y3?svg=true)](https://ci.appveyor.com/project/mattherman/mbdotnet)

# MbDotNet
A .NET client library for interacting with mountebank (www.mbtest.org). This project aims to reduce the amount of mountebank knowledge required in order to create usable stubs.

## NuGet Package ##

The library is available for install as a NuGet package.

https://www.nuget.org/packages/MbDotNet

## Usage Examples

For usage examples, see the [Usage Examples (v2)](https://github.com/mattherman/MbDotNet/wiki/Usage-Examples-%28v2%29) wiki page.

## Unsupported Functionality ##

The following Mountebank functionality is not yet supported:
- HTTPS and SMTP imposters
- Stub behaviors
- All response types other than "is"
- The "exists", "or", "and", and "inject" predicates

Pull requests are always welcome.
