using Microsoft.Extensions.Configuration;

using System.Data;
using System.Data.SqlClient;


namespace eCommerce.Infrastructure.DbContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DapperDbContext(IConfiguration configuration) 
        {
            _configuration = configuration;
             string? ConnectionString=  configuration.GetConnectionString("PostgresConnection");
            _connection = new SqlConnection(ConnectionString);
        }    

        public IDbConnection DbConnection => _connection;
    } 
}
