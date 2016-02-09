namespace EF.Interception.Tests
{
    public class PostExecuteInterceptor : Interceptor<IPostExecutedEntity>
    {
        public override void PostInsert(IContext<IPostExecutedEntity> context)
        {
            context.Entity.IsPostExecuted = true;
        }
    }
}
