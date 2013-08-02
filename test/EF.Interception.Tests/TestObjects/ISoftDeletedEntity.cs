namespace EF.Interception.Tests
{
    public interface ISoftDeletedEntity : IEntity
    {
        bool IsDeleted { get; set; }
    }
}