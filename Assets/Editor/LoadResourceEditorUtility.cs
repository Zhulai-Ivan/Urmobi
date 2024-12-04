using Cysharp.Threading.Tasks;
using UnityEditor.AddressableAssets;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Editor
{
    public static class LoadResourceEditorUtility
    {
        public static async UniTask<T> LoadAsset<T>(string key) where T : Object
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            foreach (var group in settings.groups)
            {
                foreach (var entry in group.entries)
                {
                    if (entry.address == key)
                    {
                        var result = await Addressables.LoadAssetAsync<T>(entry.address);
                        return result;
                    }
                }
            }
            return null;
        }
        
    }
}