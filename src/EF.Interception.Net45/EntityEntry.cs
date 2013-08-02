using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace EF.Interception
{
    [DebuggerDisplay("{Entity}: {State}")]
    internal class EntityEntry : IEntityEntry
    {
        private readonly DbEntityEntry _entry;

        public EntityEntry(DbEntityEntry entry)
        {
            _entry = entry;
            OriginalState = entry.State;
        }

        public EntityState OriginalState { get; private set; }

        public object Entity
        {
            get { return _entry.Entity; }
        }

        public EntityState State
        {
            get { return _entry.State; }
            set { _entry.State = value; }
        }

        public DbEntityValidationResult GetValidationResult()
        {
            return _entry.GetValidationResult();
        }
    }
}