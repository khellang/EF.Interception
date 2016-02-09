namespace EF.Interception.Tests
{
    public interface IPostExecutedEntity : IEntity
    {
        bool IsPostExecuted { get; set; }
    }
}
