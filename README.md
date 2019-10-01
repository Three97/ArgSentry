# ArgSentry

[![Build status](https://ci.appveyor.com/api/projects/status/yxa3tt4d9exdhgik?svg=true)](https://ci.appveyor.com/project/eric-davis/argsentry)
[![codecov](https://codecov.io/gh/eric-davis/ArgSentry/branch/master/graph/badge.svg)](https://codecov.io/gh/eric-davis/ArgSentry)

ArgSentry is a .NET Framework / .NET Core utility library for validating method argument values.

The concept here is that of guard clauses used as a first line of defense for parameterized constructors and methods.

For instance, if you're expecting a GUID, but your method would not make sense to be passed an empty GUID, you can use the `Prevent.EmptyGuid(parameter, nameOfParameter);` like so:

```csharp
public string GetUppercaseGuid(Guid guid)
{
    Prevent.EmptyGuid(guid, nameof(guid));

    return guid.ToString().ToUpper();
}
```

Granted, this is a very trivial example, but if the `GetUppercaseGuid` method was called with an empty GUID, ArgSentry will throw an `ArgumentException` stating that it was expecting a non-empty GUID to be passed in.
