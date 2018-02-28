using System;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader
{
    public class SimpleSqlLoaderFactory : IPluginFactory
    {
        private readonly IAbstractDbFactory _dbFactory;

        public SimpleSqlLoaderFactory(IAbstractDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public IPlugin Create()
        {
            return new SimpleSqlLoader(_dbFactory);
        }
    }
}
