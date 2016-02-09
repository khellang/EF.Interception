using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    internal class EntityEntry : IEntityEntry
    {
        public EntityEntry(DbEntityEntry entry, EntityState beforeState)
        {
            Entry = entry;
            BeforeState = beforeState;
        }

        private DbEntityEntry Entry { get; }

        public EntityState BeforeState { get; }

        public object Entity => Entry.Entity;

        public DbEntityValidationResult ValidationResult => Entry.GetValidationResult();

        public EntityState State
        {
            get { return Entry.State; }
            set { Entry.State = value; }
        }
    }
}