using Providers;
using Zenject;

namespace Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IResourcesProvider>().
                To<AddressablesProvider>()
                .AsSingle();
        }
    }
}