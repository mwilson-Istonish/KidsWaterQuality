using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.Queries
{
    public static class UserQuery
    {
        // This method returns a SQL string that selects all active users
        public static string GetAllUsers()
        {
            string sql = "Select * FROM [User] where IsActive = 1";
            return sql;
        }

        // This method returns a SQL string that selects the ID of an active user with the specified email
        public static string GetEmail()
        {
            string sql = "Select Id from [User] where Email = @Email and IsActive = 1";
            return sql;
        }

        // This method returns a SQL string that updates the login key and expiration date for an active user with the specified email
        public static string SetMagicLink()
        {
            string sql = "Update [User] set Loginkey = @Guid, LoginKeyExpiration = @TimeStamp Where Email = @Email";
            return sql;
        }

        // This method returns a SQL string that selects information about an active user with the specified login key and token, and whose login key expiration date is less than the current date/time
        public static string Login()
        {
            DateTime expire = DateTime.Now.AddHours(1); // sets expiration date/time to 1 hour from current time
            string sql = "SELECT [User].Id, [User].FirstName, [User].LastName, [User].Email, [User].WQCID, Role.Name AS Role FROM [User] INNER JOIN Role ON [User].RoleId = Role.Id where [User].Email = @Email AND LoginKey = @Token AND LoginKeyExpiration < GETDATE() AND [User].IsActive = 1";
            return sql;
        }

        public static string NewUser()
        {
            string sql = "Select lastname, firstname, email, phone, facility_orgcode as wqcid from contacts where email = @Email AND type = 'FAC'";
            return sql;
        }

        public static string AddNewProvider()
        {
            string sql = "Insert INTO [User] (roleid, firstname, lastname, email, wqcid, phone, createdby, createdat, updatedby, lastupdated, isactive) Values (@RoleId, @FirstName, @LastName, @Email, @WQCID, @Phone, @CreatedBy, @CreatedAt, @UpdatedBy, @LastUpdated, @IsActive)";            
            return sql;
        }

        //public static string GetProfile()
        //{
        //    string sql = "SELECT f.orgname as name, f.orgtype as type, f.orgcode as wqcid, f.town, a.address1 as Address1, a.address2, a.address3, a.city, a.state, a.zip FROM facilities f INNER JOIN addresses a ON f.orgcode = a.facilities_orgcode Where f.orgcode = @WQCID";
        //    return sql;
        //}

        // This method creates a new user account request that user managers can approve or disapprove
        public static string AddUserAccountRequest()
        {
            // the ip filtering isn't working due to the sql server being in a different time zone. Should be fixed at deploy
            string sql = @"IF NOT EXISTS (
                    SELECT 1
                    FROM UserAccountRequest
                    WHERE IpAddress = @IpAddress
                    AND RequestDate > DATEADD(minute, -10, GETDATE())
                    )
                    BEGIN
                        INSERT INTO UserAccountRequest (FirstName, LastName, Email, RequestDate, IpAddress)
                        VALUES (@FirstName, @LastName, @Email, @RequestDate, @IpAddress)
                    END";
            return sql;
        }

        public static string GetRateTable()
        {
            string sql = "Select Id, Action, Duration, Town as Hourly from RateTable where Id > 0 ORDER BY Id DESC";
            return sql;
        }
    }
}
