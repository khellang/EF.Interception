using System;

namespace EF.Interception.Tests
{
    public class AuditedEntity : IAuditedEntity
    {
        public int Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}