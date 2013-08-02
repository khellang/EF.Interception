using System;

namespace EF.Interception.Tests
{
    public interface ITimestamped
    {
        DateTime CreatedAt { get; set; }

        DateTime ModifiedAt { get; set; }
    }
}