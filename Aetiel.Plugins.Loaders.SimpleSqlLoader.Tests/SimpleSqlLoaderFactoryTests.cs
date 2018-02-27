using System;
using Xunit;
using Aetiel.Plugins.Loaders.SimpleSqlLoader;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader.Tests
{
    public class SimpleSqlLoaderFactoryTests
    {
        [Fact]
        public void CreateInstantiatesSimpleSqlLoader()
        {
            var factory = new SimpleSqlLoaderFactory();
            var loader = factory.Create();

            Assert.NotNull(loader);
            Assert.IsType<SimpleSqlLoader>(loader);
        }
    }
}
