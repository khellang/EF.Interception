using System;

namespace EF.Interception.Tests
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public interface IAuditedEntity : IEntity
    {
        DateTime? CreatedAt { get; set; }

        DateTime? ModifiedAt { get; set; }
    }
}