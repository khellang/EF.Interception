using System.Data.Entity;

namespace EF.Interception.Tests
{
    public class InMemoryDbContext : InterceptionDbContext
    {
        public InMemoryDbContext() : base(Effort.DbConnectionFactory.CreateTransient(), true) { }

        public DbSet<Book> Books { get; set; }
    }
}