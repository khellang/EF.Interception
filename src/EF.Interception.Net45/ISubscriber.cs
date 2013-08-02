namespace EF.Interception
{
    internal interface ISubscriber
    {
        void Intercept(EntityEntry entityEntry, bool isPostSave);
    }
}