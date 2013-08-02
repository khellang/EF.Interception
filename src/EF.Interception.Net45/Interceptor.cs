using System;
using System.Collections.Generic;
using System.Linq;

namespace EF.Interception
{
    /// <summary>
    /// Convenience base class for interceptors.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities to intercept.</typeparam>
    public abstract class Interceptor<TEntity> : IInterceptor<TEntity> where TEntity : class
    {
        private readonly IEnumerable<InterceptorMethod<TEntity>> _methods;

        protected Interceptor()
        {
            _methods = GetInterceptorMethods();
        }

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

        public void Intercept(IEntityEntry entityEntry, bool isPostSave)
        {
            if (entityEntry == null) throw new ArgumentNullException("entityEntry");

            if (!(entityEntry.Entity is TEntity)) return;

            foreach (var method in _methods.Where(m => m.CanIntercept(entityEntry.State, isPostSave)))
            {
                method.Invoke(this, entityEntry);
            }
        }

        private static IEnumerable<InterceptorMethod<TEntity>> GetInterceptorMethods()
        {
            return typeof(IInterceptor<>)
                .MakeGenericType(typeof(TEntity))
                .GetMethods()
                .Select(x => new InterceptorMethod<TEntity>(x));
        }
    }
}