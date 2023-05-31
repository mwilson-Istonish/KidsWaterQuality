using CDPHE.H20.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class RemedialActionQuery
    {
        public static string GetRemedialActions()
        {
            string sql = "SELECT Id, Name, NotToExceed FROM RemedialAction";
            return sql;
        }
    }
}
