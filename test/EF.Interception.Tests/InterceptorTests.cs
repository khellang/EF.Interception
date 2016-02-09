using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Moq;
using Xunit;

namespace EF.Interception.Tests
{
    public class InterceptorTests
    {
        public class Intercept
        {
            [Fact]
            public void ShouldCallPreInsertWhenEntityIsInserted()
            {
                DoTest(EntityState.Added, false, x =>
                    x.PreInsert(It.Is<IContext<IAuditedEntity>>(y => y.State == EntityState.Added)));
            }

            [Fact]
            public void ShouldCallPreUpdateWhenEntityIsUpdated()
            {
                DoTest(EntityState.Modified, false, x =>
                    x.PreUpdate(It.Is<IContext<IAuditedEntity>>(y => y.State == EntityState.Modified)));
            }

            [Fact]
            public void ShouldCallPreDeleteWhenEntityIsDeleted()
            {
                DoTest(EntityState.Deleted, false, x =>
                    x.PreDelete(It.Is<IContext<IAuditedEntity>>(y => y.State == EntityState.Deleted)));
            }

            [Fact]
            public void ShouldCallPostInsertAfterEntityWasInserted()
            {
                DoTest(EntityState.Added, true, x =>
                    x.PostInsert(It.Is<IContext<IAuditedEntity>>(y => y.State == EntityState.Added)));
            }

            [Fact]
            public void ShouldCallPostUpdateAfterEntityWasUpdated()
            {
                DoTest(EntityState.Modified, true, x =>
                    x.PostUpdate(It.Is<IContext<IAuditedEntity>>(y => y.State == EntityState.Modified)));
            }

            [Fact]
            public void ShouldCallPostDeleteAfterEntityWasUpdated()
            {
                DoTest(EntityState.Deleted, true, x => 
                    x.PostDelete(It.Is<IContext<IAuditedEntity>>(y => y.State == EntityState.Deleted)));
            }

            [Fact]
            public void ShouldNotCallAnythingIfEntityWasDetached()
            {
                DoTest(EntityState.Detached, true);
            }

            [Fact]
            public void ShouldNotCallAnythingIfEntityIsUnchanged()
            {
                DoTest(EntityState.Unchanged, true);
            }

            private static void DoTest(
                EntityState state,
                bool isPostSave,
                Expression<Action<IInterceptor<IAuditedEntity>>> expression = null)
            {
                // Arrange
                var entityEntry = new Mock<IEntityEntry>();
                entityEntry.SetupGet(x => x.Entity).Returns(new Book { Id = 123 });
                entityEntry.SetupGet(x => x.State).Returns(state);
                entityEntry.SetupGet(x => x.BeforeState).Returns(state);

                var interceptor = new Mock<IInterceptor<IAuditedEntity>>(MockBehavior.Strict);
                if (expression != null) interceptor.Setup(expression);

                // Act
                new MockInterceptor<IAuditedEntity>(interceptor.Object).Intercept(entityEntry.Object, isPostSave);

                // Assert
                interceptor.VerifyAll();
            }
        }
    }
}