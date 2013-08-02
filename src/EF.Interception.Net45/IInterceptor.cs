using System.Data;

namespace EF.Interception
{
    /// <summary>
    /// The interface that all interceptors have to implement.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities to intercept..</typeparam>
    public interface IInterceptor<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Called before the entity is inserted.
        /// </summary>
        /// <param name="context">The context.</param>
        [InterceptorMethod(EntityState.Added, false)]
        void PreInsert(IContext<TEntity> context);

        /// <summary>
        /// Called before the entity is updated.
        /// </summary>
        /// <param name="context">The context.</param>
        [InterceptorMethod(EntityState.Modified, false)]
        void PreUpdate(IContext<TEntity> context);

        /// <summary>
        /// Called before the entity is deleted.
        /// </summary>
        /// <param name="context">The context.</param>
        [InterceptorMethod(EntityState.Deleted, false)]
        void PreDelete(IContext<TEntity> context);

        /// <summary>
        /// Called after the entity is inserted.
        /// </summary>
        /// <param name="context">The context.</param>
        [InterceptorMethod(EntityState.Added, true)]
        void PostInsert(IContext<TEntity> context);

        /// <summary>
        /// Called after the entity is updated.
        /// </summary>
        /// <param name="context">The context.</param>
        [InterceptorMethod(EntityState.Modified, true)]
        void PostUpdate(IContext<TEntity> context);

        /// <summary>
        /// Called after the entity is deleted.
        /// </summary>
        /// <param name="context">The context.</param>
        [InterceptorMethod(EntityState.Deleted, true)]
        void PostDelete(IContext<TEntity> context);
    }
}