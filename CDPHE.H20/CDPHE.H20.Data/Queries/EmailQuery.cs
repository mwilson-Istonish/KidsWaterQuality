using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public class EmailQuery
    {
        public static string GetContactDetails()
        {
            string sql = @"declare @RequestId int;
                           set @RequestId = @RequestIdValue;

                            WITH RequestFacility AS (
                                SELECT
                                    r.Id AS RequestId,
                                    f.Name AS FacilityName,
                                    u1.FirstName + ' ' + u1.LastName AS EmployeeName,
                                    u1.Email AS EmployeeEmail,
                                    u2.FirstName + ' ' + u2.LastName AS ProviderName,
                                    u2.Email AS ProviderEmail
                                FROM [dbo].[Request] r
                                JOIN [dbo].[Facility] f ON r.FacilityId = f.Id
                                LEFT JOIN [dbo].[User] u1 ON r.IsAssignedTo = u1.id
                                LEFT JOIN [dbo].[User] u2 ON f.WQCID = u2.WQCID
                                WHERE r.Id = @RequestId
                            )

                            SELECT
                                FacilityName,
                                EmployeeName,
                                EmployeeEmail,
                                ProviderName,
                                ProviderEmail
                            FROM RequestFacility
                            WHERE RequestId = @RequestId;";
            return sql;
        }
    }
}
