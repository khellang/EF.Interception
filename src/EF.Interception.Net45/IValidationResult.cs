using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    public interface IValidationResult : IHideObjectMembers
    {
        IEnumerable<DbValidationError> Errors { get; }

        bool IsValid { get; }
    }
}