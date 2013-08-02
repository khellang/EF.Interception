using System.Data;

namespace EF.Interception
{
    public interface IInterceptor<in TEntity> where TEntity : class
    {
        [InterceptorMethod(EntityState.Added, false)]
        void PreInsert(IContext<TEntity> context);

        [InterceptorMethod(EntityState.Modified, false)]
        void PreUpdate(IContext<TEntity> context);

        [InterceptorMethod(EntityState.Deleted, false)]
        void PreDelete(IContext<TEntity> context);

        [InterceptorMethod(EntityState.Added, true)]
        void PostInsert(IContext<TEntity> context);

        [InterceptorMethod(EntityState.Modified, true)]
        void PostUpdate(IContext<TEntity> context);

        [InterceptorMethod(EntityState.Deleted, true)]
        void PostDelete(IContext<TEntity> context);
    }
}