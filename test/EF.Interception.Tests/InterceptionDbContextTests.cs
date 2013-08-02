using System;
using System.Data.Entity.Validation;

using Xunit;

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

            [Fact]
            public void ShouldThrowOnInvalidEntities()
            {
                _context.Books.Add(new Book { Name = "Harry Potter and the Philosopher's Stone" });
                
                Assert.Throws<DbEntityValidationException>(() => _context.SaveChanges());
            }

            [Fact]
            public void ShouldNotThrowOnInvalidEntitiesWhenInterceptorSetsRequiredFields()
            {
                _context.AddInterceptor(new AuditInterceptor());
                _context.Books.Add(new Book { Name = "Harry Potter and the Chamber of Secrets" });
                
                Assert.DoesNotThrow(() => _context.SaveChanges());
            }

            [Fact]
            public void ShouldNotDeleteEntitiesWhenStateIsSetToModifiedDuringInterception()
            {
                _context.AddInterceptor(new SoftDeleteInterceptor());

                var book = new Book
                {
                    Name = "Harry Potter and the Prisoner of Azkaban",
                    CreatedAt = DateTime.Now, // Need these for validation...
                    ModifiedAt = DateTime.Now
                };

                _context.Books.Add(book);
                _context.SaveChanges();

                _context.Books.Remove(book);
                _context.SaveChanges();

                var otherBook = _context.Books.Find(book.Id);

                Assert.Same(book, otherBook);
                Assert.True(book.IsDeleted);
            }

            public void Dispose()
            {
                _context.Dispose();
            }
        }
    }
}