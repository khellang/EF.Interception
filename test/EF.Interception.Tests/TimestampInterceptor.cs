using System;

namespace EF.Interception.Tests
{
    public class TimestampInterceptor : Interceptor<ITimestamped>
    {
        public override void PreInsert(IContext<ITimestamped> context)
        {
            context.Entity.ModifiedAt = DateTime.Now;
            context.Entity.CreatedAt = DateTime.Now;
        }

        public override void PreUpdate(IContext<ITimestamped> context)
        {
            context.Entity.ModifiedAt = DateTime.Now;
        }
    }
}