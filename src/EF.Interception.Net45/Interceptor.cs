namespace EF.Interception
{
    public abstract class Interceptor<TEntity> : IInterceptor<TEntity> where TEntity : class
    {
        public virtual void PreInsert(IContext<TEntity> context) { }

        public virtual void PreUpdate(IContext<TEntity> context) { }

        public virtual void PreDelete(IContext<TEntity> context) { }

        public virtual void PostInsert(IContext<TEntity> context) { }

        public virtual void PostUpdate(IContext<TEntity> context) { }

        public virtual void PostDelete(IContext<TEntity> context) { }
    }
}