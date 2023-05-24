using CDPHE.H20.Data.Context;
using CDPHE.H20.Data.Models;
using CDPHE.H20.Data.Queries;
using CDPHE.H20.Data.ViewModels;
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
        public Task<User> GetUserById(int id);
        public Task<bool> EmailUser(string email);
        public Task<UserRole> Login(string userguid, string token);
        public Task<string> AddUser(UserRole newUser);
        public Task<FacilityVM> GetFacility(string wqcid);
    }

    public class UserService : IUserService
    {
        private readonly DapperContext _dbContext = new DapperContext();
        private readonly DapperMySQLContext _dbMySQLContext = new DapperMySQLContext();

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

        public async Task<User> GetUserById(int id)
        {
            User user = new User();

            return user;
            
        }
        
        public async Task<bool> EmailUser(string email)
        {
            // Check to see if the email exists in the User table
            var query = UserQuery.GetEmail();
            bool isValidEmail = false;

            Random random = new Random();
            int randomNumber = random.Next(0, 1000000); // Generate a random number between 0 and 999999.
            string sixDigitNumber = randomNumber.ToString("D6"); // Format the number with leading zeroes to make it 6 digits long.

            using (var connection = _dbContext.CreateConnection())
            {
                var response = await connection.QueryAsync<int>(query, new { Email = email});
                if(response.Count() > 0)
                {
                    isValidEmail = true;
                }
                else
                {
                    // Check Main DB
                    var _newUser = await NewUser(email);
                    if(_newUser != null)
                    {
                        var result = AddUser(_newUser);
                        if(result.Result == "Success")
                        {
                            isValidEmail = true;
                        }
                        else
                        {
                            isValidEmail = false;
                        }
                    }
                }
            }

            // If exists, generate a GUID for logging in
            if (isValidEmail)
            {
                query = UserQuery.SetMagicLink();
                // Set timestamp for logging in and insert
                using(var connection = _dbContext.CreateConnection())
                {
                    // Set LoginToken
                    try
                    {
                        var setLoginToken = await connection.ExecuteAsync(query, new { Guid = sixDigitNumber, TimeStamp = DateTime.Now.AddMinutes(10), Email = email });
                        
                        // Send Email
                        EmailService emailService = new EmailService();
                        var response = await emailService.SendLoginEmail(email, sixDigitNumber);
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                    }
                    
                }
            }

            return isValidEmail;
        }

        public async Task<string> AddUser(UserRole user)
        {
            string message = "Success";
            var query = UserQuery.AddNewProvider();
            try
            {
                using (var connection = _dbContext.CreateConnection())
                {
                    var newRow = await connection.ExecuteAsync(query, new { RoleId = 1, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, WQCID = user.WQCID, Phone = user.Phone, CreatedBy = 1, CreatedAt = DateTime.Now, UpdatedBy = 1, LastUpdated = DateTime.Now, IsActive = 1 });
                }
            }
            catch (Exception)
            {
                message = "Failed";
            }

            return message;
        }

        public async Task<FacilityVM> GetFacility(string wqcid)
        {
            var query = UserQuery.GetFacility();
            using (var connection = _dbMySQLContext.CreateConnection())
            {
                var facility = await connection.QueryFirstAsync<FacilityVM>(query, new { Wqcid = wqcid });
                return facility;
            }
        }

        public async Task<UserRole> Login(string email, string tempkey)
        {
            var query = UserQuery.Login();

            using (var connection = _dbContext.CreateConnection())
            {
                var userRole = await connection.QueryFirstOrDefaultAsync<UserRole>(query, new { Email = email, Token = tempkey });
                return userRole;
            }
        }

        public async Task<UserRole> NewUser(string email)
        {
            var query = UserQuery.NewUser();  

            using (var connection = _dbMySQLContext.CreateConnection())
            {
                var userRole = connection.Query<UserRole>(query, new { Email = email });
                return userRole.FirstOrDefault();
            }
        }
    }
}
