# ATypeScanner
An appeasing type scanner that can scan for implementations of interfaces and classes

![ATypeScanner](icon.png)

## Search for implementations of a class or interface
```cs
var matches = new TypeScanner(typeof(IMyInterface).Assembly)
    .FindImplementationsOf<IMyInterface>();
```

## Search for implementations of a open class or interface
```cs
var matches = new TypeScanner(typeof(IMyInterface<>).Assembly)
    .FindClosingImplementationsOf(typeof(IMyInterface<>));
```
