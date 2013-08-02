using System.Data;

namespace EF.Interception
{
    /// <summary>
    /// The context for an event.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity.</typeparam>
    public interface IContext<out TEntity> : IHideObjectMembers
    {
        TEntity Entity { get; }

        EntityState State { get; set; }
    }
}