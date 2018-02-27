using System;
using Aetiel.Plugins.Interfaces;
using System.Collections.Generic;
using Dapper;
using System.Data.Common;
using System.Reflection;
using System.Linq;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader
{
    public class SimpleSqlLoader : LoadPlugin
    {
        public override bool Load(IPluginParams pluginParams, IEnumerable<object> transformed)
        {
            var loaderParams = pluginParams as SimpleSqlLoaderParams;
            if (loaderParams == null)
            {
                throw new ArgumentException($"{this.GetType().Name} expects parameters of ${nameof(SimpleSqlLoaderParams)} type.");
            }

            var errors = new List<SimpleSqlLoaderParams.Error>();
            using (var connection = loaderParams.ConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var model in transformed)
                    {
                        var type = model.GetType();
                        var tableName = type.Name;
                        var fields = type.GetProperties(BindingFlags.Public).Select(x => x.Name).ToArray();
                        var parameters = fields.Select(x => $"@{x}");
                        try
                        {
                            connection.Execute(
                                $"INSERT INTO {tableName}({string.Join(",", fields)}) VALUES(${string.Join(",", parameters)})",
                                model
                            );
                        }
                        catch (Exception ex)
                        {
                            errors.Add(new SimpleSqlLoaderParams.Error { Exception = ex, Object = model });
                        }
                    }
                    transaction.Commit();
                }
            }

            loaderParams.Errors.AddRange(errors);
            return !loaderParams.Errors.Any();
        }
    }
}
