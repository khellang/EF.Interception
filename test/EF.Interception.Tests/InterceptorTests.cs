using System;
using System.Data;
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

                var interceptor = new Mock<IInterceptor<IAuditedEntity>>(MockBehavior.Strict);
                if (expression != null) interceptor.Setup(expression);

                // Act
                new MockInterceptor<IAuditedEntity>(interceptor.Object).Intercept(entityEntry.Object, isPostSave);

                // Assert
                interceptor.VerifyAll();
            }
        }
    }

    internal class MockInterceptor<TEntity> : Interceptor<TEntity> where TEntity : class
    {
        private readonly IInterceptor<TEntity> _interceptor;

        public MockInterceptor(IInterceptor<TEntity> interceptor)
        {
            _interceptor = interceptor;
        }

        public override void PreInsert(IContext<TEntity> context)
        {
            _interceptor.PreInsert(context);
        }

        public override void PreUpdate(IContext<TEntity> context)
        {
            _interceptor.PreUpdate(context);
        }

        public override void PreDelete(IContext<TEntity> context)
        {
            _interceptor.PreDelete(context);
        }

        public override void PostInsert(IContext<TEntity> context)
        {
            _interceptor.PostInsert(context);
        }

        public override void PostUpdate(IContext<TEntity> context)
        {
            _interceptor.PostUpdate(context);
        }

        public override void PostDelete(IContext<TEntity> context)
        {
            _interceptor.PostDelete(context);
        }
    }
}