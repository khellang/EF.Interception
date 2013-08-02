using System.Data;

namespace EF.Interception
{
    /// <summary>
    /// The interception context.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities to intercept.</typeparam>
    public interface IContext<out TEntity> : IHideObjectMembers
    {
        TEntity Entity { get; }

        EntityState State { get; set; }
    }
}