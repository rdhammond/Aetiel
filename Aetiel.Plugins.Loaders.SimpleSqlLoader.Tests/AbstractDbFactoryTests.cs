using System;
using System.Data;
using Moq;
using Xunit;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader.Tests
{
    public class AbstractDbFactoryTests
    {
        [Fact]
        public void RegisterAcceptsDbTypeAndFactory()
        {
            var factory = new AbstractDbFactory();
            factory.Register("Test", new Mock<IDbFactory>().Object);
        }

        [Fact]
        public void RegisterThrowsExceptionIfAlreadyRegistered()
        {
            const string NAME = "TestFactory";
            var factory = new AbstractDbFactory();
            factory.Register(NAME, new Mock<IDbFactory>().Object);
            Assert.Throws<ArgumentException>(() => factory.Register(NAME, new Mock<IDbFactory>().Object));
        }

        [Fact]
        public void CreateInstantiatesForKnownFactory()
        {
            const string NAME = "Testing";
            var connection = new Mock<IDbConnection>();
            var dbFactory = new Mock<IDbFactory>();
            dbFactory.Setup(x => x.Create(It.IsAny<string>())).Returns(() => connection.Object);

            var factory = new AbstractDbFactory();
            factory.Register(NAME, dbFactory.Object);
            Assert.Same(connection.Object, factory.Create(NAME, "XYZ"));
        }

        [Fact]
        public void CreateThrowsExceptionIfUnknownFactory()
        {
            var factory = new AbstractDbFactory();
            Assert.Throws<ArgumentException>(() => factory.Create("ABC", "123"));
        }
    }
}