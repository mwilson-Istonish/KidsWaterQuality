using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class RoleQuery
    {
        public static string GetAllRoles()
        {
            string sql = "SELECT * FROM [dbo].[Role]";
            return sql;
        }
    }
}
