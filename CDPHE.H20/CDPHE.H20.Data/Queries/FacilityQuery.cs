using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public class FacilityQuery
    {
        public static string GetFacilityById()
        {
            // SQL Server Database
            string sql = "SELECT Id,WQCID,Name,Address1,Address2,Address3,City,County,State,ZipCode FROM [dbo].[Facility] WHERE WQCID = @Id;";
            return sql;
        }

        public static string GetFacilityByWQCID()
        {
            // MySQL Database
            string sql = "SELECT f.orgname as name, f.orgtype as type, f.orgcode as wqcid, f.town as County, a.address1 as Address1, a.address2, a.address3, a.city, a.state, a.zip as ZipCode FROM facilities f INNER JOIN addresses a ON f.orgcode = a.facilities_orgcode Where f.orgcode = @WQCID";
            return sql;
        }

        public static string InsertFacility()
        {
            string sql = "INSERT INTO Facility(WQCID,Name,Address1,Address2,Address3,City,County,State,ZipCode,CreatedBy,CreatedAt,UpdatedBy,LastUpdated,IsActive) VALUES (@WQCID,@Name,@Address1,@Address2,@Address3,@City,@County,@State,@ZipCode,@CreatedBy,@CreatedAt,@UpdatedBy,@LastUpdated,@IsActive); Select @@Identity";
            return sql;
        }
    }
}
