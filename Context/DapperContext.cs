using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        /*Getting the Connection String from APPSETTING.JSON FILE*/
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        /*Supplying the Connection When Needed by this Method*/
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
