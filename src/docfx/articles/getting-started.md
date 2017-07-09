﻿# DynamicRestProxy - Getting Started

This article describes the basic conventions of the DynamicRextProxy api.
See [advanced topics](advanced.md) for mechanisms to bypass these conventions.

## Basic Usage

Using the dynamic rest client start with instantiating an instance, accessing an endpoint path and invoking a REST verb, awaiting the result. Always declare the `DynamicRestClient` as
a `dynamic`.

    dynamic client = new DynamicRestClient("http://dev.virtualearth.net/REST/v1/");
    dynamic result = await proxy.Locations.get(postalCode: "55116", countryRegion: "US", key: "api-key");
[Figure 1]

### Building the endpoint path

The endpoint path is represented by dot-separated members of the dynamic client instance. Each node in the path is another dynamic object
to which additional path elements can be chained. The resulting path is relative to the base address set in the constructor of the client object.

The full endpoint Uri of the example in Figure 1 is:

http://dev.virtualearth.net/REST/v1/Locations/

    dynamic google = new DynamicRestClient("https://www.googleapis.com/");
    dynamic bucket = await google.storage.v1.b("uspto-pair").get();
[Figure 2]

The code in Figure 2 chains multiple elements together to build a longer path. It also uses an escape mechanism in order to specify a
path element that is not a valid idenifier in C#. The resulting Uri is:

https://www.googleapis.com/storage/v1/b/uspto-pair/

### Passing parameters

Parameters are based to the verb method invocation using [C#'s named parameter syntax](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#named-arguments).

Any type of object can be passed as a parameter value which are serialized via the value object's
[ToString](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring?view=netframework-4.7)
method. Both parameter names and values are [Url encoded](https://docs.microsoft.com/en-us/dotnet/api/system.net.webutility.urlencode?view=netframework-4.7)

The GET request for the example in Figure 1 is:
http://dev.virtualearth.net/REST/v1/Locations/?postalCode=55116&countryRegion=US&key=api-key

Named parameters passed to a POST invocation will be [form url encoded](http://www.w3.org/TR/html401/interact/forms.html#h-17.13.4.1) in the request body.

### Passing Content

Request content is passed to the verb invocation as an unnamed argument. The first unnamed argument will be passed as the request
content body. Subsequent unnamed arguments, [with the exception of some special types](advanced.md), will be ignored.

- Strings and primitive types will be passed as [StringContent](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.stringcontent.-ctor?view=netframework-4.7)
- Byte arrays will be passed as [ByteArrayContent](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.bytearraycontent?view=netframework-4.7)
- Any type of [stream](http://msdn.microsoft.com/query/dev15.query?appId=Dev15IDEF1&l=EN-US&k=k(System.IO.Stream);k(DevLang-csharp)&rd=true) will be passed as [StreamContent](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.streamcontent.-ctor?view=netframework-4.7)
- All other types will be serialized to JSON

#### Setting content headers

### Invoking the Http verb

GET, PUT, POST, DELETE and PATCH are the http verbs supported by this REST client. Invocation of the verb method
sends the appropraite http message to the endpoint, along with defaults, parameters and content. Verb methods are always
lower case and return a `Task` object, so must be `await`-ed. Unless using a strongly typed response
(see below), the return will be `Task&lt;object>` where the result type is a dynamic object.

## Setting Defaults

### Api Keys

### Authentication and Authorization