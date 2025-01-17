namespace Brady.Infrastructure.Interfaces
{
    public interface ILoadRepository<T> where T : class
    {
        public T LoadXml(string fileName);
    }
}
