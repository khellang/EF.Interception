using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    internal class EntityEntry : IEntityEntry
    {
        private readonly DbEntityEntry _entry;

        public EntityEntry(DbEntityEntry entry)
        {
            _entry = entry;
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

        public DbEntityValidationResult GetValidationResult()
        {
            return _entry.GetValidationResult();
        }
    }
}