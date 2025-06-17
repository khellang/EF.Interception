# EF.Interception [![Build status](https://ci.appveyor.com/api/projects/status/701q9uw8kker5kqx)](https://ci.appveyor.com/project/khellang/ef-interception)

EF.Interception is small library that lets you intercept saving of entities in Entity Framework.

## Intallation

Install the plugin from NuGet: `Install-Package EF.Interception`.

## Usage

To enable interception, derive your database context from `InterceptionDbContext` instead of `DbContext`:

```csharp
public class MyDbContext : InterceptionDbContext
{
    // -- SNIP --
}
```

You can then add interceptors by calling `AddInterceptor`. An interceptor must derive from `Interceptor<T>`:

```csharp
public class AuditInterceptor : Interceptor<IAuditEntity>
{
    public override void PreInsert(IContext<IAuditEntity> context)
    {
        context.Entity.CreatedAt = DateTime.UtcNow;
        context.Entity.ModifiedAt = DateTime.UtcNow;
    }

    public override void PreUpdate(IContext<IAuditEntity> context)
    {
        context.Entity.ModifiedAt = DateTime.UtcNow;
    }
}

public class MyDbContext : InterceptionDbContext
{
    public MyDbContext()
    {
        AddInterceptor(new AuditInterceptor());
    }
}
```

`Interceptor<T>` has six methods you can override:
 - `PreInsert`
 - `PreUpdate`
 - `PreDelete`
 - `PostInsert`
 - `PostUpdate`
 - `PostDelete`

All methods takes an `IContext<T>` which has three properties:
 - `Entity` - The entity which is intercepted.
 - `State` - The entity's current state. This can be altered.
 - `ValidationResult` - The entity's validation result.

## Sponsors

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=khellang&utm_medium=EF.Interception) and [Dapper Plus](https://dapper-plus.net/?utm_source=khellang&utm_medium=EF.Interception) are major sponsors and proud to contribute to the development of EF.Interception.

[![Entity Framework Extensions](https://raw.githubusercontent.com/khellang/khellang/refs/heads/master/.github/entity-framework-extensions-sponsor.png)](https://entityframework-extensions.net/bulk-insert?utm_source=khellang&utm_medium=EF.Interception)

[![Dapper Plus](https://raw.githubusercontent.com/khellang/khellang/refs/heads/master/.github/dapper-plus-sponsor.png)](https://dapper-plus.net/bulk-insert?utm_source=khellang&utm_medium=EF.Interception)
