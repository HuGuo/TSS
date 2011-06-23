namespace TSS.BLL
{
    public class RepositoryFactory<T>
        where T : new()
    {
        public static T Get()
        {
            return new T();
        }
    }
}
