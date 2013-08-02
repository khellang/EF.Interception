# EF.Interception


EF.Interception is small library that lets you intercept saving of entities in Entity Framework 5.

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
 - PreInsert
 - PreUpdate
 - PreDelete
 - PostInsert
 - PostUpdate
 - PostDelete

All methods takes an `IContext<T>` which has three properties:
 - Entity - The entity which is intercepted.
 - State - The entity's current state. This can be altered.
 - ValidationResult - The entity's validation result.
