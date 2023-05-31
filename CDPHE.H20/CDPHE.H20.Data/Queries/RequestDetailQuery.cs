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
            string sql = "SELECT Request.Id, Request.Status, Request.CreatedAt, Facility.Name AS Facility, [User].FirstName + ' ' + [User].LastName AS Provider, [User].Email, [User].Phone, Facility.WQCID, Facility.Address1, Facility.Address2, Facility.City, Facility.State, Facility.ZipCode FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.UserId = [User].id WHERE Request.Id = @ID" +
                         " SELECT rd.Id AS RequestDetailId, rd.SampleName, rd.InitialSampleDate, rd.SampleResultOperator, rd.InitialSampleResult, rd.FlushSampleDate, rd.FlushResultOperator, rd.FlushSampleResult, rd.RemedialActionId, rd.MaterialCost, rd.MaterialLabor, RemedialAction.Name AS RemedialAction, RemedialAction.Id AS RemedialActionID FROM RequestDetail AS rd INNER JOIN Request AS r ON rd.RequestId = r.Id INNER JOIN RemedialAction ON rd.RemedialActionId = RemedialAction.Id WHERE (r.Id = @ID) AND (rd.IsActive = 1)";
            return sql;
        }

        public static string GetRequestsByProviderId()
        {
            string sql = "SELECT Request.Id AS Id, Facility.Name as Facility, [User].FirstName + ' ' + [User].LastName AS Provider, [User].Email, Request.CreatedAt, Request.Status FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.UserId = [User].id WHERE Request.UserId = @Id";
            return sql;
        }

        public static string GetRequests()
        {
            string sql = "SELECT Request.Id AS Id, Facility.Name as Facility, [User].FirstName + ' ' + [User].LastName AS Provider, [User].Email, Request.CreatedAt, Request.Status FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.UserId = [User].id";
            return sql;
        }

        public static string GetRequestTotalCost()
        {
            string sql = "SELECT Sum(RequestDetail.MaterialCost) + Sum(RequestDetail.MaterialLabor) as TotalCost FROM Request INNER JOIN RequestDetail ON Request.Id = RequestDetail.RequestId Where RequestId = @RequestId";
            return sql;
        }
    }
}
