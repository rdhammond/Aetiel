using System;
using Aetiel.Plugins.Interfaces;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Linq;
using System.Data;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader
{
    public class SimpleSqlLoader : LoadPlugin
    {
        private readonly IAbstractDbFactory _dbFactory;

        public SimpleSqlLoader(IAbstractDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public override bool Load(IPluginParams pluginParams, IEnumerable<object> transformed)
        {
            var loaderParams = pluginParams as SimpleSqlLoaderParams;
            if (loaderParams == null)
            {
                throw new ArgumentException($"{this.GetType().Name} expects parameters of ${nameof(SimpleSqlLoaderParams)} type.");
            }
            var errors = new List<SimpleSqlLoaderParams.Error>();

            using (var db = _dbFactory.Create(loaderParams.DbType, loaderParams.ConnectionString))
            {
                foreach (var entity in transformed)
                {
                    try
                    {
                        // ** TODO: Upsert based on more sophisticated keys.
                        InsertEntity(db, entity);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(new SimpleSqlLoaderParams.Error(entity, ex));
                    }
                }
            }

            loaderParams.Errors = errors.AsReadOnly();
            return !loaderParams.Errors.Any();
        }

        private static void InsertEntity(IDbConnection db, object entity)
        {
            var type = entity.GetType();
            var fields = type.GetProperties(BindingFlags.Public).Select(x => x.Name);

            using (var command = db.CreateCommand())
            {
                command.CommandText = CreateInsertStatement(type.Name, fields);
                command.ExecuteNonQuery();
            }
        }

        private static string CreateInsertStatement(string tableName, IEnumerable<string> fields)
        {
            fields = fields as string[] ?? fields.ToArray();
            var properties = fields.Select(x => $"{x}");

            return $"INSERT INTO {tableName}({string.Join(", ", fields)}) VALUES({string.Join(", ", properties)})";
        }
    }
}
