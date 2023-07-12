using CDPHE.H20.Data.Context;
using CDPHE.H20.Data.Models;
using CDPHE.H20.Data.Queries;
using CDPHE.H20.Data.ViewModels;
using Dapper;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public Task AddUserAccountRequest(UserAccountRequest userAccountRequest);
        public Task<string> GetProfile(string wqcid, int userId);
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

                var responseList = response.ToList();
                if (responseList.Any()) 
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
                        
                        // TODO: Uncomment for Production
                        // !! Removed for Testing !!
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

        public async Task<string> GetProfile(string wqcid, int userId)
        {
            ProfileDetails profile = new ProfileDetails();
            // var query = UserQuery.GetProfile();

            FacilityDetails facilityInfo = new FacilityDetails();

            // Check to see if the Facility exists in the MSSQL db
            var existingFacility = await GetFacility(wqcid);

            // If not exists, get info from MySQL db
            if(existingFacility == null)
            {
                var query3 = FacilityQuery.GetFacilityByWQCID();
                using (var myconnection = _dbMySQLContext.CreateConnection())
                {
                    // Get New Facility from MySQL and...
                    var facility = await myconnection.QueryFirstAsync<FacilityDetails>(query3, new { Wqcid = wqcid });

                    // Insert into MSSQL
                    FacilityService facilityService = new FacilityService();
                    var newFacility = await facilityService.AddFacility(facility, userId);
                    facilityInfo = facility;
                    facilityInfo.Id = newFacility;
                }
            }
            else
            {
                facilityInfo = existingFacility;
            }

            profile.WQCID = wqcid;
            profile.Name = facilityInfo.Name;
            profile.Town = facilityInfo.County;
            profile.FacilityId = facilityInfo.Id;
            profile.Address.Address1 = facilityInfo.Address1;
            profile.Address.Address2 = facilityInfo.Address2;
            profile.Address.Address3 = facilityInfo.Address3;
            profile.Address.City = facilityInfo.City;
            profile.Address.State = facilityInfo.State;
            profile.Address.ZipCode = facilityInfo.ZipCode;

            var query2 = UserQuery.GetRateTable();
            query2 = query2.Replace("Town", "[" + profile.Town + "]");

            using (var connection = _dbContext.CreateConnection())
            {
                var rateTable = await connection.QueryAsync<RateTable>(query2);
                string hourlyRate = "0.00";
                int i = 0;
                foreach (var rate in rateTable)
                {
                    RateTable _rateTable = new RateTable();
                    _rateTable.Id = rate.Id;
                    _rateTable.Action = rate.Action;
                    _rateTable.Duration = rate.Duration;
                    if (i == 0)
                    {
                        hourlyRate = String.Format("{0:C2}", (Convert.ToDecimal(rate.Hourly) * Convert.ToDecimal(1.00)));
                        _rateTable.Hourly = hourlyRate;
                        _rateTable.NotToExceed = String.Format("{0:C2}", (Convert.ToDecimal(0.00)));
                        i++;
                    }
                    else
                    {
                        _rateTable.NotToExceed = String.Format("{0:C2}", (Convert.ToDecimal(rate.Hourly) * Convert.ToDecimal(1.00)));
                        _rateTable.Hourly = hourlyRate;
                    }
                    
                    profile.RateTable.Add(_rateTable);
                }
            }

            return JsonConvert.SerializeObject(profile);
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

        public async Task AddUserAccountRequest(UserAccountRequest userAccountRequest)
        {
            var query = UserQuery.AddUserAccountRequest();
            using (var connection = _dbContext.CreateConnection())
            {
                // create request. SQL query will handle spam
                try
                {
                    await connection.ExecuteAsync(query, userAccountRequest);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                }
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

        public async Task<FacilityDetails> GetFacility(string wqcid)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var query = FacilityQuery.GetFacilityById();
                try
                {
                    var facility = await connection.QueryFirstOrDefaultAsync<FacilityDetails>(query, new { Id = wqcid });
                    return facility;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                
            }
        }
    }
}
