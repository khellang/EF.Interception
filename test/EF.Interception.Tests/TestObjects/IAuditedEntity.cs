using System;

namespace EF.Interception.Tests
{
    public interface IAuditedEntity : IEntity
    {
        DateTime? CreatedAt { get; set; }

        DateTime? ModifiedAt { get; set; }
    }
}