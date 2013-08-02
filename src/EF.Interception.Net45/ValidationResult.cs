using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    internal class ValidationResult : IValidationResult
    {
        private readonly DbEntityValidationResult _result;

        public ValidationResult(DbEntityValidationResult result)
        {
            _result = result;
        }

        public IEnumerable<DbValidationError> Errors
        {
            get { return _result.ValidationErrors; }
        }

        public bool IsValid
        {
            get { return _result.IsValid; }
        }
    }
}