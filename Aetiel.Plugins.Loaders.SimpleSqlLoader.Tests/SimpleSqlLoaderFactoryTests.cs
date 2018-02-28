using System;
using Xunit;
using Aetiel.Plugins.Loaders.SimpleSqlLoader;
using Moq;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader.Tests
{
    public class SimpleSqlLoaderFactoryTests
    {
        [Fact]
        public void CreateInstantiatesSimpleSqlLoader()
        {
            var dbFactory = new Mock<IAbstractDbFactory>();
            var factory = new SimpleSqlLoaderFactory(new Mock<IAbstractDbFactory>().Object);
            var loader = factory.Create();

            Assert.NotNull(loader);
            Assert.IsType<SimpleSqlLoader>(loader);
        }
    }
}
