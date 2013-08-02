using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    /// <summary>
    /// Validation result for an entity.
    /// </summary>
    public interface IValidationResult : IHideObjectMembers
    {
        /// <summary>
        /// Gets the errors, if any.
        /// </summary>
        /// <value>The errors, if any.</value>
        IEnumerable<DbValidationError> Errors { get; }

        /// <summary>
        /// Gets a value indicating whether the entity is valid.
        /// </summary>
        /// <value><c>true</c> if the entity is valid; otherwise, <c>false</c>.</value>
        bool IsValid { get; }
    }
}