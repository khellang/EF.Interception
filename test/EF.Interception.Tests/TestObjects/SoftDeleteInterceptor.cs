using System.Data;

namespace EF.Interception.Tests
{
    public class SoftDeleteInterceptor : Interceptor<ISoftDeletedEntity>
    {
        public override void PreDelete(IContext<ISoftDeletedEntity> context)
        {
            context.Entity.IsDeleted = true;
            context.State = EntityState.Modified;
        }
    }
}