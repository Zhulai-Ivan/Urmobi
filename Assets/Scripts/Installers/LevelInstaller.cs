using Loaders;
using Providers;
using Zenject;

namespace Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IResourcesProvider>()
                .To<AddressablesProvider>()
                .AsSingle();

            Container.Bind<IDataLoader>()
                .To<JsonLoader>()
                .AsSingle();
            
           Container.Bind<LevelLoader>()
               .AsSingle();
        }
    }
}