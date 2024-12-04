using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Providers
{
    public class AddressablesProvider : IResourcesProvider
    {
        public async UniTask<T> LoadAsync<T>(string key)
        {
            var handle = Addressables.LoadAssetAsync<T>(key);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
                return handle.Result;
            
            throw new Exception($"Unable to load {key} from Addressables");
        }

        public UniTask ReleaseAsync<T>(T asset)
        {
           Addressables.Release(asset);
           return UniTask.CompletedTask;
        }
    }
}