using System.Data;
using System.Data.SqlClient;

namespace bpoLabs.DbContext
{
    public class DapperDbContext
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("bpoLabsDB");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

    }
}
