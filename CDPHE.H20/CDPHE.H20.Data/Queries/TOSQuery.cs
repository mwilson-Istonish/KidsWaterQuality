using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class TOSQuery
    {
        public static string GetTermsOfService()
        {
            string sql = "Select Body from TermsOfService WHERE Id = 1";
            return sql;
        }

        public static string UpdateTermsOfService()
        {
            string sql = "Update TermsOfService set Body = @Body WHERE Id = 1";
            return sql;
        }
    }
}
