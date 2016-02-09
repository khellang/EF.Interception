using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    internal class ValidationResult : IValidationResult
    {
        public ValidationResult(DbEntityValidationResult result)
        {
            Result = result;
        }

        private DbEntityValidationResult Result { get; }

        public IEnumerable<DbValidationError> Errors => Result.ValidationErrors;

        public bool IsValid => Result.IsValid;
    }
}