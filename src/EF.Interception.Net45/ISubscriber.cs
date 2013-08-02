using System.Data.Entity.Infrastructure;

namespace EF.Interception
{
    internal interface ISubscriber
    {
        void Intercept(IEntityEntry entityEntry, bool isPostSave);
    }
}