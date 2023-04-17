using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Context
{
    public class DapperContext
    {
        private string _connectionString;

        public DapperContext()
        {
            _connectionString = "Server=rds.amazonaws.com;Database=;User Id=;Password=;TrustServerCertificate=True;";
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
