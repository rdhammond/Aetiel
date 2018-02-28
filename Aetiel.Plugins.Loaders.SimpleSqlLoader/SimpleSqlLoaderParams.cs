using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader
{
    public class SimpleSqlLoaderParams : IPluginParams
    {
        public class Error
        {
            public object Entity { get; }
            public Exception Exception { get; }

            public Error(object entity, Exception exception)
            {
                Entity = entity;
                Exception = exception;
            }
        }

        public string DbType { get; }
        public string ConnectionString { get; }
        public IReadOnlyCollection<Error> Errors { get; set; }

        public SimpleSqlLoaderParams(string dbType, string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}