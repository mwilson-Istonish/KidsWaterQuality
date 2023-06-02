using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class RequestQuery
    {
        public static string GetAllRequests()
        {
            string sql = "SELECT * FROM [dbo].[Request] where IsActive = 1";
            return sql;
        }

        public static string GetRequestById()
        {
            string sql = "SELECT * FROM [dbo].[Request] WHERE Id = @Id;";
            return sql;
        }

        public static string UpdateRequest()
        {
            string sql = "UPDATE [dbo].[Request] SET UserId = @UserId, FacilityId = @FacilityId, UpdatedBy = @UpdatedBy, LastUpdated = @LastUpdated, IsActive = @IsActive WHERE Id = @Id;";
            return sql;
        }

        public static string InsertNewRequest()
        {
            string sql = "INSERT INTO Request(UserId,FacilityId,Status,CreatedBy,CreatedAt,UpdatedBy,LastUpdated,IsActive) VALUES (@UserId,@FacilityId,@Status,@CreatedBy,@CreatedAt,@UpdatedBy,@LastUpdated,@IsActive); Select @@Identity";
            return sql;
        }

        public static string DeleteRequest()
        {
            string sql = "UPDATE [dbo].[Request] SET IsActive = 0, Status = 'Canceled', UpdatedBy = @UserId, LastUpdated = @Now  WHERE Id = @Id;";
            return sql;
        }

        public static string GetUserAccountRequests()
        {
            string sql = "SELECT * FROM UserAccountRequest";
            return sql;
        }

        public static string GetBudget() 
        {
            string sql = "SELECT TOP 1 * FROM Budget";
            return sql;
        }

    }
}
