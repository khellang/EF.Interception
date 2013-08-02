using System.Data;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    /// <summary>
    /// Describes the entity entry from the DbContext.
    /// </summary>
    public interface IEntityEntry : IHideObjectMembers
    {
        /// <summary>
        /// Gets the entity object.
        /// </summary>
        /// <value>The entity object.</value>
        object Entity { get; }

        /// <summary>
        /// Gets or sets the entity's state.
        /// </summary>
        /// <value>The entity's state.</value>
        EntityState State { get; set; }

        /// <summary>
        /// Gets the entity's validation result.
        /// </summary>
        /// <value>The entity's validation result.</value>
        DbEntityValidationResult ValidationResult { get; }
    }
}