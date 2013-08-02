using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EF.Interception
{
    /// <summary>
    /// DbContext with interception support.
    /// </summary>
    public abstract class InterceptionDbContext : DbContext
    {
        private readonly List<ISubscriber> _subscribers = new List<ISubscriber>();

        private readonly object _lock = new object();

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

        public void AddInterceptor<TEntity, TInterceptor>() 
            where TEntity : class
            where TInterceptor : IInterceptor<TEntity>, new()
        {
            AddInterceptor(new TInterceptor());
        }

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