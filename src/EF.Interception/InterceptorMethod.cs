using System;
using System.Data.Entity;
using System.Reflection;

namespace EF.Interception
{
    internal class InterceptorMethod<TEntity> where TEntity : class
    {
        private readonly MethodInfo _method;

        private readonly EntityState _state;

        private readonly bool _isAfterSave;

        private readonly Type _contextType;

        public InterceptorMethod(MethodInfo method)
        {
            var attribute = method.GetCustomAttribute<InterceptorMethodAttribute>(true);
            if (attribute == null)
            {
                throw new InvalidOperationException(
                    "Interceptor methods must be marked with an InterceptorMethodAttribute.");
            }

            _method = method;
            _state = attribute.State;
            _isAfterSave = attribute.IsPostSave;
            _contextType = typeof(Context<>).MakeGenericType(typeof(TEntity));
        }

        public bool CanIntercept(EntityState state, bool isPostSave)
        {
            return _state == state && _isAfterSave == isPostSave;
        }

        public void Invoke(Interceptor<TEntity> target, IEntityEntry entityEntry)
        {
            _method.Invoke(target, new[] { Activator.CreateInstance(_contextType, entityEntry) });
        }
    }
}