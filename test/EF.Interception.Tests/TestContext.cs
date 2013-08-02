using System.Data.Entity;

namespace EF.Interception.Tests
{
    public class TestContext : InterceptionDbContext
    {
        public TestContext()
        {
            Database.Delete();
            AddInterceptor<ITimestamped, TimestampInterceptor>();
        }

        public DbSet<TimestampedEntity> Entities { get; set; }
    }
}