namespace EF.Interception
{
    internal interface IInterceptor
    {
        void Intercept(IEntityEntry entityEntry, bool isPostSave);
    }

    internal interface IInterceptor<in TEntity> : IInterceptor where TEntity : class
    {
        void PreInsert(IContext<TEntity> context);

        void PreUpdate(IContext<TEntity> context);

        void PreDelete(IContext<TEntity> context);

        void PostInsert(IContext<TEntity> context);

        void PostUpdate(IContext<TEntity> context);

        void PostDelete(IContext<TEntity> context);
    }
}