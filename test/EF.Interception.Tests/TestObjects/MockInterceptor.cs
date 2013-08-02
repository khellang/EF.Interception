namespace EF.Interception.Tests
{
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