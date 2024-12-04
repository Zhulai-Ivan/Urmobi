using Cysharp.Threading.Tasks;

namespace Providers
{
    public interface IResourcesProvider
    {
        UniTask<T> LoadAsync<T>(string key);
        UniTask ReleaseAsync<T>(T asset);
    }
}