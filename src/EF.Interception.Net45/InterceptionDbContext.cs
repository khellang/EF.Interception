using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;

namespace EF.Interception
{
    /// <summary>
    /// A DbContext with support for intercepting entities before and after they're saved.
    /// </summary>
    public abstract class InterceptionDbContext : DbContext
    {
        private readonly List<ISubscriber> _subscribers = new List<ISubscriber>();

        private readonly object _lock = new object();

        protected InterceptionDbContext() { }

        protected InterceptionDbContext(DbCompiledModel model) : base(model) { }

        protected InterceptionDbContext(
            DbConnection existingConnection,
            DbCompiledModel model,
            bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection) { }

        protected InterceptionDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection) { }

        protected InterceptionDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext) { }

        protected InterceptionDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        protected InterceptionDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model) { }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(IsChanged)
                .Select(entry => new EntityEntry(entry))
                .ToList();

            Intercept(modifiedEntries, false);

            var result = base.SaveChanges();

            Intercept(modifiedEntries, true);

            return result;
        }

        /// <summary>
        /// Adds an interceptor to the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities to intercept.</typeparam>
        /// <typeparam name="TInterceptor">The type of the interceptor.</typeparam>
        public void AddInterceptor<TEntity, TInterceptor>() 
            where TEntity : class
            where TInterceptor : IInterceptor<TEntity>, new()
        {
            AddInterceptor(new TInterceptor());
        }

        /// <summary>
        /// Adds the given interceptor to the context.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities to intercept.</typeparam>
        /// <param name="interceptor">The interceptor.</param>
        /// <exception cref="System.ArgumentNullException">If the interceptor is <c>null</c></exception>
        public void AddInterceptor<TEntity>(IInterceptor<TEntity> interceptor) where TEntity : class
        {
            if (interceptor == null) throw new ArgumentNullException("interceptor");

            lock (_lock)
            {
                _subscribers.Add(new Subscriber<TEntity>(interceptor));
            }
        }

        private static bool IsChanged(DbEntityEntry entry)
        {
            return entry.State != EntityState.Unchanged && entry.State != EntityState.Detached;
        }

        private void Intercept(IList<EntityEntry> entityEntries, bool isPostSave)
        {
            lock (_lock)
            {
                foreach (var subscriber in _subscribers)
                {
                    foreach (var entityEntry in entityEntries)
                    {
                        subscriber.Intercept(entityEntry, isPostSave);
                    }
                }
            }
        }
    }
}