namespace TSS.BLL
{
    public class RepositoryFactory<T> where T : new()
    {
        //private static readonly T instance = new T();

        public static T Get()
        {
            return new T();
        }
    }
}
