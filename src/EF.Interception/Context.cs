using System.Data.Entity;

namespace EF.Interception
{
    internal class Context<T> : IContext<T> 
    {
        private readonly IEntityEntry _entry;

        public Context(IEntityEntry entry)
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

        public IValidationResult ValidationResult
        {
            get { return new ValidationResult(_entry.ValidationResult); }
        }
    }
}