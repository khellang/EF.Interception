using System;

using Xunit;

namespace EF.Interception.Tests
{
    public class Tests
    {
        [Fact]
        public void Testing()
        {
            var context = new TestContext();
            var entity = new TimestampedEntity { Name = "Kristian" };

            context.Entities.Add(entity);
            context.SaveChanges();

            entity.Name = "Kristian Hellang";
            context.SaveChanges();

            Assert.Equal(entity.CreatedAt.Date, DateTime.Today);
            Assert.Equal(entity.ModifiedAt.Date, DateTime.Today);
        }
    }
}