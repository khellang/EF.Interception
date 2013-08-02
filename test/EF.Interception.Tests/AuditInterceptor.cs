using System;

namespace EF.Interception.Tests
{
    public class AuditInterceptor : Interceptor<IAuditedEntity>
    {
        public override void PreInsert(IContext<IAuditedEntity> context)
        {
            context.Entity.ModifiedAt = DateTime.Now;
            context.Entity.CreatedAt = DateTime.Now;
        }

        public override void PreUpdate(IContext<IAuditedEntity> context)
        {
            context.Entity.ModifiedAt = DateTime.Now;
        }
    }
}