namespace EF.Interception
{
    /// <summary>
    /// Convenience base class for interceptors.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities to intercept.</typeparam>
    public abstract class Interceptor<TEntity> : IInterceptor<TEntity> where TEntity : class
    {
        /// <summary>
        /// Called before the entity is inserted.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void PreInsert(IContext<TEntity> context) { }

        /// <summary>
        /// Called before the entity is updated.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void PreUpdate(IContext<TEntity> context) { }

        /// <summary>
        /// Called before the entity is deleted.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void PreDelete(IContext<TEntity> context) { }

        /// <summary>
        /// Called after the entity is inserted.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void PostInsert(IContext<TEntity> context) { }

        /// <summary>
        /// Called after the entity is updated.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void PostUpdate(IContext<TEntity> context) { }

        /// <summary>
        /// Called after the entity is deleted.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void PostDelete(IContext<TEntity> context) { }
    }
}