using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
namespace CDPHE.H20.Data.Context
{
    public class DapperMySQLContext
    {
        private string _connectionString;

        public DapperMySQLContext()
        {
            _connectionString = "";
        }

        public IDbConnection CreateConnection() 
            => new MySqlConnection( _connectionString );
    }
}
