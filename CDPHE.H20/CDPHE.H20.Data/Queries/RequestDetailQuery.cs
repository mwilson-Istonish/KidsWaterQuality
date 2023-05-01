using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class RequestDetailQuery
    {
        public static string GetRequestDetailsByRequestId()
        {
            string sql = "SELECT Request.Id as Id, Request.Status, Request.CreatedAt, Facility.Name as Facility, [User].FirstName + ' ' + [User].LastName as Provider FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.Id = [User].id WHERE Request.Id = @ID" +
                         " SELECT rd.Id AS RequestDetailId, rd.SampleName, rd.InitialSampleDate, rd.SampleResultOperator, rd.InitialSampleResult, rd.FlushSampleDate, rd.FlushResultOperator, rd.FlushSampleResult, rd.RemedialActionId, rd.MaterialCost, rd.MaterialLabor, RemedialAction.Name FROM RequestDetail AS rd INNER JOIN Request AS r ON rd.RequestId = r.Id INNER JOIN RemedialAction ON rd.RemedialActionId = RemedialAction.Id WHERE (r.Id = @ID) AND (rd.IsActive = 1)";
            return sql;
        }
    }
}
