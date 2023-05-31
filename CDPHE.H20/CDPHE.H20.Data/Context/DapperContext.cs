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
            _connectionString = "Server=istonish-cdphe-dev-1.cd13cvdflbu2.us-east-2.rds.amazonaws.com;Database=cdpheh20;User Id=cdpheadmin;Password=dDtbrxYSKU75M!;TrustServerCertificate=True;";
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
