using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader
{
    public class AbstractDbFactory : IAbstractDbFactory
    {
        private readonly IDictionary<string, IDbFactory> _factories = new Dictionary<string, IDbFactory>();

        public void Register(string dbType, IDbFactory factory)
        {
            if (_factories.ContainsKey(dbType))
            {
                throw new ArgumentException("${type.Name} is already registered.");
            }
            _factories.Add(dbType, factory);
        }

        public IDbConnection Create(string providerName, string connectionString)
        {
            if (!_factories.TryGetValue(providerName, out var provider))
            {
                throw new ArgumentException($"Unknown provider type {providerName}.");
            }
            return provider.Create(connectionString);
        }
    }
}