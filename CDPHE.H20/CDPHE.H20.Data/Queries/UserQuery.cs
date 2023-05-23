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
            string sql = "SELECT [User].Id, [User].FirstName, [User].LastName, [User].Email, Role.Name AS Role FROM [User] INNER JOIN Role ON [User].RoleId = Role.Id where [User].Email = @Email AND LoginKey = @Token AND LoginKeyExpiration < GETDATE() AND [User].IsActive = 1";
            return sql;
        }

        public static string NewUser()
        {
            string sql = "";
            return sql;
        }
    }
}
