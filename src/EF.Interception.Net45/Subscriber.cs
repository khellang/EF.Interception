using System.Collections.Generic;
using System.Linq;

namespace EF.Interception
{
    internal class Subscriber<TEntity> : ISubscriber where TEntity : class
    {
        private readonly IInterceptor<TEntity> _interceptor;

        private readonly IEnumerable<InterceptorMethod<TEntity>> _methods;

        public Subscriber(IInterceptor<TEntity> interceptor)
        {
            _interceptor = interceptor;
            _methods = GetInterceptorMethods();
        }

        public void Intercept(EntityEntry entityEntry, bool isPostSave)
        {
            if (entityEntry.Entity is TEntity)
            {
                var state = entityEntry.OriginalState;
                var methods = _methods.Where(m => m.CanIntercept(state, isPostSave));

                foreach (var method in methods)
                {
                    method.Invoke(_interceptor, entityEntry);
                }
            }
        }

        private static IEnumerable<InterceptorMethod<TEntity>> GetInterceptorMethods()
        {
            return typeof(IInterceptor<>)
                .MakeGenericType(typeof(TEntity))
                .GetMethods()
                .Select(method => new InterceptorMethod<TEntity>(method));
        }
    }
}