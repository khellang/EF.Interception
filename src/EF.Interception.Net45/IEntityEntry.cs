using System.Data;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    internal interface IEntityEntry
    {
        EntityState OriginalState { get; }

        object Entity { get; }

        EntityState State { get; set; }

        DbEntityValidationResult GetValidationResult();
    }
}