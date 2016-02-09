using System.Data.Entity;

namespace EF.Interception
{
    internal class Context<T> : IContext<T> 
    {
        public Context(IEntityEntry entry)
        {
            Entry = entry;
        }

        private IEntityEntry Entry { get; }

        public T Entity => (T) Entry.Entity;

        public IValidationResult ValidationResult => new ValidationResult(Entry.ValidationResult);

        public EntityState State
        {
            get { return Entry.State; }
            set { Entry.State = value; }
        }
    }
}