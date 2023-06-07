using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class StatusQuery
    {
        public static string GetRequestsByStatus()
        {
            string sql = "SELECT Request.Id, Facility.WQCID, Request.Status, Facility.Name AS Facility, [User].FirstName + ' ' + [User].LastName AS Provider, [User].Email, Facility.City, Facility.County, Facility.State, Facility.ZipCode, (SELECT Sum(RequestDetail.ActualMaterialCost) + Sum(RequestDetail.ActualLaborCost) FROM RequestDetail WHERE RequestDetail.RequestId = Request.Id AND RequestDetail.IsActive = 1) AS TotalCost, Request.CreatedAt FROM Request INNER JOIN Facility ON Request.FacilityId = Facility.Id INNER JOIN [User] ON Request.UserId = [User].id WHERE Request.Status = @Status";
            return sql;
        }

        public static string UpdateRequestStatus()
        {
            string sql = "Update Request set Status = @Status, UpdatedBy = @UserId, LastUpdated = @DateTime WHERE Id = @Id";
            return sql;
        }
    }
}
