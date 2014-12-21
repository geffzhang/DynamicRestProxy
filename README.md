DynamicRestProxy
================

A rest client proxy using the .NET [Dynamic Language Runtime](http://msdn.microsoft.com/en-us/library/dd233052(v=vs.110).aspx). 

This is a set of classes that wrap a concrete implementation of http client communication with a [DynamicObject](http://msdn.microsoft.com/en-us/library/system.dynamic.dynamicobject(v=vs.110).aspx). The wrapper translates dynamic method invocations and endpoint paths into REST requests. 

All requests are asynynchronous and return dynamic objects.

The intent is to make it easier to access REST API's from C# without needing to create strongly typed API wrappers and numerous static POCO types for basic DTO responses. 

So a GET statement can be as simple as:

    dynamic google = new DynamicRestClient("https://www.googleapis.com/");
    dynamic bucket = await google.storage.v1.b("uspto-pair").get();
    Console.WriteLine(bucket.location);

Supports the GET, POST, PUT, PATCH and DELETE verbs.

There is a [NuGet package](https://www.nuget.org/packages/DynamicRestProxy/) for the abstract base class to allow different http client libraries to be used. Current concrete implementations include: 
- A dynamic proxy for the [RestSharp RestClient](http://restsharp.org/). 
- A dynamic proxy for Microsoft's [Portable HttpClient](https://www.nuget.org/packages/Microsoft.Net.Http/). This also has a [NuGet package](https://www.nuget.org/packages/DynamicRestClient/).
- A [dynamic rest client](https://github.com/dkackman/DynamicRestProxy/wiki/Using-the-DynamicRestClient) that reduces to a bare minimum the amount of code needed to start calling rest endpoints

Example usage is on the [Wiki](https://github.com/dkackman/DynamicRestProxy/wiki) as well as supplied in the unit test projects.


If you try to run the unit tests take a close look at the CredentialStore class in the unit test project. It's pretty straighforward and you can use it to supply your own api keys while keeping them out of the code.

