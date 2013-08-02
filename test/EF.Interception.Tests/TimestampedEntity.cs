using System;

namespace EF.Interception.Tests
{
    public class TimestampedEntity : ITimestamped
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}