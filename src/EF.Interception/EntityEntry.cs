using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    internal class EntityEntry : IEntityEntry
    {
        private readonly DbEntityEntry _entry;

        private readonly EntityState _beforeState;

        public EntityEntry(DbEntityEntry entry, EntityState beforeState)
        {
            _entry = entry;
            _beforeState = beforeState;
        }

        public object Entity
        {
            get { return _entry.Entity; }
        }

        public EntityState State
        {
            get { return _entry.State; }
            set { _entry.State = value; }
        }

        public EntityState BeforeState
        {
            get { return _beforeState; }
        }

        public DbEntityValidationResult ValidationResult
        {
            get { return _entry.GetValidationResult(); }
        }
    }
}