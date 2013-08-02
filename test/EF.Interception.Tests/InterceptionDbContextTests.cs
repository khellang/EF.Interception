using System;

namespace EF.Interception.Tests
{
    public class InterceptionDbContextTests
    {
        public class SaveChanges : IDisposable
        {
            private readonly InMemoryDbContext _context;

            public SaveChanges()
            {
                _context = new InMemoryDbContext();
            }

            // TODO: Test InterceptionDbContext here...

            public void Dispose()
            {
                _context.Dispose();
            }
        }
    }
}