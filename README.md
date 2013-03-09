Fluent Logging
==============

A small set of classes and methods for accessing .Net logging frameworks using a fluent API.

Usage
-------------

The `LogEntry` and `LogFormatter` types provide the majority of the functionality, with `ILogFacade` being used to wrap your logger. 
A facade for log4net's `ILog` type is included, as well as extension methods to simplify things a bit.

Use the fluent log4net namespace:

```csharp
using FluentLogging.Core.log4net;
```
  
Create an `ILog` instance:
  
```csharp
private static readonly ILog _log = LogManager.GetLogger(typeof(MyType));
```
  
Log things:
  
```csharp
public void DoSomething(int id) {
  try {  

    //Do something here
    
  } catch(Exception e) {
    _log
      .NewEntry()
      .WithMessage("An error was encountered doing something")
      .WithException(e)
      .WithItem("id", id)
      .AsError();
  }
}
```
