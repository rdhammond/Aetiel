using Moq;
using System.Reflection;
using Aetiel.Plugins.Factories;
using Xunit;
using Aetiel.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aetiel.Plugins.Tests
{
    public class AbstractPluginFactoryTests
    {
        private class PluginFactoryStub : IPluginFactory
        {
            public IPlugin Create(IPluginParams pluginParams)
            {
                return null;
            }
        }

        public class IPluginStub : IPlugin
        { }

        public class FactoryStub<T> : IPluginFactory
            where T : class, IPlugin
        {
            public IPlugin Create(IPluginParams pluginParams)
            {
                return new Mock<T>().Object;
            }
        }
        private class StubParams : IPluginParams
        { }

        public static IEnumerable<object[]> TestMethod(string name)
        {
            Console.WriteLine(name);
            Console.WriteLine(typeof(AbstractPluginFactoryInstance).GetMethod(name));
            return new[] { new object[] { typeof(AbstractPluginFactoryInstance).GetMethod(name) } };
        }

        private Action UnwrapInvocationException(Action action)
        {
            return () => {
                try
                {
                    action();
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            };
        }

        [Fact]
        public void RegisterAddsToInternalRegistry()
        {
            const string NAME = "TestName";
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<PluginFactoryStub>(NAME);
            Assert.True(factory.IsRegistered(NAME));
        }

        [Fact]
        public void DoubleRegisterThrowsException()
        {
            const string NAME = "DoubleRegister";
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<PluginFactoryStub>(NAME);
            Assert.Throws<ArgumentException>(() => factory.Register<PluginFactoryStub>(NAME));
        }

        [Fact]
        public void IsRegisteredReturnsFalseIfNotRegistered()
        {
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<PluginFactoryStub>("TestFactory");
            Assert.False(factory.IsRegistered("NotRegistered"));
        }

        [Fact]
        public void IsTypeReturnsFalseIfNotRegistered()
        {
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<PluginFactoryStub>("TestFactory");
            Assert.False(factory.IsType<PluginFactoryStub>("NotRegistered"));
        }

        [Fact]
        public void IsTypeReturnsFalseIfDifferentType()
        {
            const string NAME = "DifferentType";
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<PluginFactoryStub>(NAME);
            Assert.False(factory.IsType<int>(NAME));
        }


        [Fact]
        public void IsTypeReturnsTrueIfSameType()
        {
            const string NAME = "SameType";
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<PluginFactoryStub>(NAME);
            Assert.True(factory.IsType<PluginFactoryStub>(NAME));
        }

        [Theory]
        [MemberData(nameof(TestMethod), nameof(AbstractPluginFactoryInstance.CreateExtractor))]
        [MemberData(nameof(TestMethod), nameof(AbstractPluginFactoryInstance.CreateTransformer))]
        [MemberData(nameof(TestMethod), nameof(AbstractPluginFactoryInstance.CreateLoader))]
        public void CreateThrowsExceptionIfFactoryNotRegistered(MethodInfo testMethod)
        {
            var factory = new AbstractPluginFactoryInstance();
            Assert.Throws<ArgumentException>(UnwrapInvocationException(() =>
                testMethod.Invoke(factory, new object[] { "UnregisteredName", new StubParams() })
            ));
        }

        [Theory]
        [MemberData(nameof(TestMethod), nameof(AbstractPluginFactoryInstance.CreateExtractor))]
        [MemberData(nameof(TestMethod), nameof(AbstractPluginFactoryInstance.CreateTransformer))]
        [MemberData(nameof(TestMethod), nameof(AbstractPluginFactoryInstance.CreateLoader))]
        public void CreateThrowsExceptionIfUnexpectedTypeCreated(MethodInfo testMethod)
        {
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<FactoryStub<IPluginStub>>("Other");
            Assert.Throws<ArgumentException>(UnwrapInvocationException(
                () => testMethod.Invoke(factory, new object[] { "Other", new StubParams() })
            ));
        }

        [Fact]
        public void CreateExtractorReturnsInstanceOnSuccess()
        {
            const string NAME = "TestExtractor";
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<FactoryStub<IExtractPlugin>>(NAME);

            var result = factory.CreateExtractor(NAME, new StubParams());
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateTransformerReturnsInstanceOnSuccess()
        {
            const string NAME = "TestTransformer";
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<FactoryStub<ITransformPlugin>>(NAME);

            var result = factory.CreateTransformer(NAME, new StubParams());
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateLoaderReturnsInstanceOnSuccess()
        {
            const string NAME = "TestLoader";
            var factory = new AbstractPluginFactoryInstance();
            factory.Register<FactoryStub<ILoadPlugin>>(NAME);

            var result = factory.CreateLoader(NAME, new StubParams());
            Assert.NotNull(result);
        }
    }
}