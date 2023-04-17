using CDPHE.H20.Data.Context;
using CDPHE.H20.Data.Models;
using CDPHE.H20.Data.Queries;
using Dapper;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        public Task<bool> EmailUser(string email);
        public Task<string> Login(string userguid, string token);
    }

    public class UserService : IUserService
    {
        private readonly DapperContext _dbContext = new DapperContext();

        public UserService()
        {
            
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var query = UserQuery.GetAllUsers();
            using (var connection = _dbContext.CreateConnection())
            {
                var Users = await connection.QueryAsync<User>(query);
                return Users.ToList();

            }
        }

        public User GetUserById(int id)
        {
            User user = new User();

            return user;
            
        }
        
        public async Task<bool> EmailUser(string email)
        {
            // Check to see if the email exists in the User table
            var query = UserQuery.GetEmail();
            bool isValidEmail = false;
            Guid g = Guid.NewGuid();

            using (var connection = _dbContext.CreateConnection())
            {
                var response = await connection.QueryAsync<int>(query, new { Email = email});
                if(response != null)
                {
                    isValidEmail = true;
                }
            }

            // If exists, generate a GUID for logging in
            if (isValidEmail)
            {
                query = UserQuery.SetMagicLink();
                // Set timestamp for logging in and insert
                using(var connection = _dbContext.CreateConnection())
                {
                    // Set MagicLink
                    try
                    {
                        var magicLink = await connection.ExecuteAsync(query, new { Guid = g.ToString(), TimeStamp = DateTime.Now, Email = email });
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                    }
                    
                }
            }

            return isValidEmail;
        }

        public void AddUser(User user)
        {
            
        }

        public void UpdateUser(User user)
        {
            
        }

        public void DeleteUser(int id)
        {
            
        }

        public async Task<string> Login(string userguid, string token)
        {
            var query = UserQuery.Login();

            using (var connection = _dbContext.CreateConnection())
            {
                var UserId = await connection.QueryAsync<int>(query, new { Guid = userguid, Token = token });
                return UserId.ToString();
            }
        }
    }
}
