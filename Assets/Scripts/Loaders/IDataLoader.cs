namespace Loaders
{
    public interface IDataLoader
    {
        T Load<T>(string key);
    }
}