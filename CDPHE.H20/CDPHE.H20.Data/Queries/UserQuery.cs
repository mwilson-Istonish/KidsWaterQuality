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
        public static string GetAllUsers()
        {
            string sql = "Select * FROM [User] where IsActive = 1";
            return sql;
        }

        public static string GetEmail()
        {
            string sql = "Select Id from [User] where Email = @Email and IsActive = 1";
            return sql;
        }

        public static string SetMagicLink()
        {
            string sql = "Update [User] set Loginkey = @Guid, LoginKeyExpiration = @TimeStamp Where Email = @Email";
            return sql;
        }

        public static string Login()
        {
            string sql = "SELECT [User].Id, [User].FirstName, [User].LastName, [User].Email, Role.Name AS Role FROM [User] INNER JOIN Role ON [User].RoleId = Role.Id where Guid = @Guid and LoginKey = @Token and IsActive = 1";
            return sql;
        }
    }
}
