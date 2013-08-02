using System.Data;
using System.Data.Entity.Validation;

namespace EF.Interception
{
    internal interface IEntityEntry
    {
        object Entity { get; }

        EntityState State { get; set; }

        DbEntityValidationResult GetValidationResult();
    }
}