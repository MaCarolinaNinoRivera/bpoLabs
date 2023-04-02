using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;

namespace bpoLabs.Common.Database
{
    public interface IDapperWrapper
    {
        IEnumerable<T> Query<T>(IDbConnection connection, string sql);
        Task ExecuteAsync(IDbConnection connection, string sql);
    }
    [ExcludeFromCodeCoverage]
    public class DapperWrapper : IDapperWrapper
    {
        public async Task ExecuteAsync(IDbConnection connection, string sql)
        {
            await connection.ExecuteAsync(sql);
        }

        public IEnumerable<T> Query<T>(IDbConnection connection, string sql)
        {
            return connection.Query<T>(sql);
        }


    }
}
