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
    public interface IStatusService
    {

        public Task<string> UpdateStatus(int id, int userId, string newStatus);
        public Task<List<RequestsVM>> GetRequestsByStatus(string newStatus);
    }

    public class StatusService : IStatusService
    {
        private readonly DapperContext _dbContext = new DapperContext();

        public StatusService()
        {

        }

        public async Task<List<RequestsVM>> GetRequestsByStatus(string newStatus)
        {
            string msg = "{ Success }";

            var query = StatusQuery.GetRequestsByStatus();
            var listRequests = new List<RequestsVM>();

            using (var connection = _dbContext.CreateConnection())
            {
                var reqList = await connection.QueryAsync<RequestsVM>(query, new { Status = newStatus });
                foreach (var request in reqList)
                {
                    listRequests.Add(request);
                }
            }

            return listRequests;
        }

        public async Task<string> UpdateStatus(int id, int userId, string newStatus)
        {
            string msg = "{ Success }";

            var query = StatusQuery.UpdateRequestStatus();

            using (var connection = _dbContext.CreateConnection())
            {
                var updateStatus = await connection.ExecuteAsync(query, new { Id = id, UserId = userId, Status = newStatus, DateTime = DateTime.Now });
                return msg;
            }
        }
    }
       
}
