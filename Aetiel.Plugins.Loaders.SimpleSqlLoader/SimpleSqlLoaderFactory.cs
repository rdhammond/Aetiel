using System;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader
{
    public class SimpleSqlLoaderFactory : IPluginFactory
    {
        public IPlugin Create()
        {
            return new SimpleSqlLoader();
        }
    }
}
