using System.Data;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader
{
    public interface IAbstractDbFactory
    {
        void Register(string dbType, IDbFactory factory);
        IDbConnection Create(string dbType, string connectionString);
    }
}