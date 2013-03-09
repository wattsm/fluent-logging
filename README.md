Fluent Logging
==============

A small set of classes and methods for accessing .Net logging frameworks using a fluent API.

Usage (log4net)
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

This will print a message in the log file similar to:

`Message = An error was encountered doing something], [Exception = (exception message)], [id = (id value)]`

Formatting
-------------
The default formatter uses a key/value pair format with each pair wrapped in square brackets. You can change the
the formatting used by creating a class which implements `ILogFormatter` and then setting it as the current formatter:

```csharp
LogFormatter.Set(
  new MyFormatter()
);
```
