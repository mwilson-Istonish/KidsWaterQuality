using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class BudgetQuery
    {
        public static string GetBudget()
        {
            string sql = "SELECT Request.Id, Request.Status, RequestDetail.MaterialCost, RequestDetail.LaborCost FROM Request RIGHT OUTER JOIN RequestDetail ON Request.Id = RequestDetail.RequestId" +
                         " Select DollarAmount from Budget where Id = 1";
            return sql;
        }
    }
}
