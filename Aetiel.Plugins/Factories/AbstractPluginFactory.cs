using System;
using System.Collections.Concurrent;
using System.Reflection;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins.Factories
{
    public static class AbstractPluginFactory
    {
        public static AbstractPluginFactoryInstance Instance => new AbstractPluginFactoryInstance();
    }

    public class AbstractPluginFactoryInstance
    {
        private readonly ConcurrentDictionary<string,ConstructorInfo> _registry = new ConcurrentDictionary<string, ConstructorInfo>();

        public void Register<T>(string name) where T : class, IPluginFactory, new()
        {
            var constructor = typeof(T).GetConstructor(Type.EmptyTypes);
            if (!_registry.TryAdd(name, constructor))
            {
                throw new ArgumentException($"{name} is already registered as a plugin factory.");
            }
        }

        public bool IsRegistered(string name)
        {
            return _registry.ContainsKey(name);
        }

        public bool IsType<T>(string name)
        {
            return _registry.TryGetValue(name, out var ctor)
                ? ctor.DeclaringType == typeof(T)
                : false;
        }

        public IExtractPlugin CreateExtractor(string name, IPluginParams extractParams)
        {
            return Create<IExtractPlugin>(name, extractParams);
        }

        public ITransformPlugin CreateTransformer(string name, IPluginParams transformParams)
        {
            return Create<ITransformPlugin>(name, transformParams);
        }

        public ILoadPlugin CreateLoader(string name, IPluginParams loadParams)
        {
            return Create<ILoadPlugin>(name, loadParams);
        }

        private TExpected Create<TExpected>(string name, IPluginParams pluginParams)
            where TExpected : IPlugin
        {
            if (!_registry.TryGetValue(name, out var factoryCtor))
            {
                throw new ArgumentException($"{name} is not a registered plugin factory.");
            }

            var factory = (IPluginFactory)factoryCtor.Invoke(null);
            var instance = factory.Create(pluginParams);
            if (!(instance is TExpected))
            {
                (instance as IDisposable)?.Dispose();
                throw new ArgumentException($"{name} is not a type of ${typeof(TExpected).Name}.");
            }
            return (TExpected)instance;
        }
    }
}