using System.Data;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace EF.Interception
{
    [DebuggerDisplay("{Entity}: {State}")]
    internal class EntityEntry
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
    }
}