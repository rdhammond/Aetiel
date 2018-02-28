using System.Data;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader
{
    public interface IDbFactory
    {
        IDbConnection Create(string connectionString);
    }
}