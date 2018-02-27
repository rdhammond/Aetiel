using System;
using System.Collections.Generic;
using System.Data.Common;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader
{
    public class SimpleSqlLoaderParams : IPluginParams
    {
        public class Error
        {
            public Exception Exception { get; set; }
            public object Object { get; set; }
        }

        public DbProviderFactory ConnectionFactory { get; }
        public string ConnectionString { get; }
        public List<Error> Errors { get; } = new List<Error>();

        public SimpleSqlLoaderParams(DbProviderFactory connectionFactory, string connectionString)
        {
            ConnectionFactory = connectionFactory;
            ConnectionString = connectionString;
        }
    }
}