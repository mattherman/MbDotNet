#!/bin/sh

rm *.nupkg
dotnet pack ./MbDotNet/MbDotNet.csproj -c Release -o ../