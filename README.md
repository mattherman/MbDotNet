[![Build status](https://ci.appveyor.com/api/projects/status/q5rn71ncmimgg3y3?svg=true)](https://ci.appveyor.com/project/mattherman/mbdotnet)

# MbDotNet
A .NET client library for interacting with mountebank (www.mbtest.org). This project aims to reduce the amount of mountebank knowledge required in order to create usable stubs.

## NuGet Package ##

The library is available for install as a NuGet package.

https://www.nuget.org/packages/MbDotNet

## Usage Examples

For usage examples, see the [Usage Examples (v1)](https://github.com/mattherman/MbDotNet/wiki/Usage-Examples-%28v1%29) wiki page.

## Unsupported Functionality ##

Stubs
- Behaviors

Reponses
- All response types other than "is"

Predicates
- All predicate types other than "equals"
