using System.Data;
using System.Diagnostics;

namespace EF.Interception
{
    [DebuggerDisplay("{Entity}: {State}")]
    internal class Context<T> : IContext<T> 
    {
        private readonly EntityEntry _entry;

        public Context(EntityEntry entry)
        {
            _entry = entry;
        }

        public T Entity
        {
            get { return (T) _entry.Entity; }
        }

        public EntityState State
        {
            get { return _entry.State; }
            set { _entry.State = value; }
        }
    }
}