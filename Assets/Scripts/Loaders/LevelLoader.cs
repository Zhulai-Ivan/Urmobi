using System;
using Data;
using Zenject;

namespace Loaders
{
    public class LevelLoader : IDisposable
    {
        private IDataLoader _dataLoader;

        [Inject]
        private void InstallBindings(IDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public LevelData Initialize(string level) => 
            _dataLoader.Load<LevelData>(level);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}