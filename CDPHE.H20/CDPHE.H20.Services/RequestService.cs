using CDPHE.H20.Data.Context;
using CDPHE.H20.Data.Queries;
using CDPHE.H20.Data.ViewModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CDPHE.H20.Data.Models;
using Azure.Core;

namespace CDPHE.H20.Services
{
    public interface IRequestService
    {
        public Task<string> GetRequestAndDetails(int id);
        public Task<List<RequestsVM>> GetRequestsByProvider(int id);
        public Task<List<RemedialActionVM>> GetRemedialActions();
        public Task<List<UserAccountRequest>> GetAccountCreationRequests();
        public Task<Budget> GetBudget();
    }
    public class RequestService : IRequestService
    {
        private readonly DapperContext _dbContext = new DapperContext();

        public RequestService() { }

        public async Task<string> GetRequestAndDetails(int id)
        {
            RequestAndDetails requestAndDetails = new RequestAndDetails();
            var query = RequestDetailQuery.GetRequestDetailsByRequestId();

            using (var connection = _dbContext.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query, new { ID = id}))
                {
                    var _request = await multi.ReadFirstAsync<RequestAndDetails>();
                    var _requestDetails = await multi.ReadAsync<ReqDetails>();
                    requestAndDetails.Id= _request.Id;
                    requestAndDetails.Status = _request.Status;
                    requestAndDetails.CreatedAt = _request.CreatedAt;
                    requestAndDetails.Facility = _request.Facility;
                    requestAndDetails.Address1 = _request.Address1;
                    if (requestAndDetails.Address2 != null)
                    {
                        requestAndDetails.Address2 = _request.Address2;
                    }
                    
                    requestAndDetails.City= _request.City;
                    requestAndDetails.State = _request.State;
                    requestAndDetails.ZipCode = _request.ZipCode;
                    requestAndDetails.WQCID= _request.WQCID;
                    requestAndDetails.Provider = _request.Provider;
                    requestAndDetails.Email = _request.Email;
                    requestAndDetails.Phone = _request.Phone;

                    foreach(var _details in _requestDetails)
                    {
                        requestAndDetails.Details.Add(_details);
                        requestAndDetails.TotalCostMaterials = (decimal)(_details.MaterialCost + requestAndDetails.TotalCostMaterials);
                        requestAndDetails.TotalCostLabor = (decimal)(_details.MaterialLabor + requestAndDetails.TotalCostLabor);
                        requestAndDetails.TotalCost = requestAndDetails.TotalCostMaterials + requestAndDetails.TotalCostLabor;
                    }
                }
            }

            return JsonConvert.SerializeObject(requestAndDetails);
        }

        public async Task<List<RequestsVM>> GetRequestsByProvider(int id)
        {
            List<RequestsVM> requests = new List<RequestsVM>();
            var query = RequestDetailQuery.GetRequestsByProviderId();
            using (var connection = _dbContext.CreateConnection())
            {
                var _requests = await connection.QueryAsync<RequestsVM>(query, new { Id = id });

                foreach (var _request in _requests)
                {
                    decimal totalCost = GetTotalCostByRequestId(_request.Id).Result;
                    _request.TotalCost = totalCost.ToString();
                    requests.Add(_request);
                }
            }

            return requests;

        }
        
        public async Task<List<RequestsVM>> GetAllRequests()
        {
            List<RequestsVM> requests = new List<RequestsVM>();
            var query = RequestDetailQuery.GetRequests();
            using (var connection = _dbContext.CreateConnection())
            {
                var _requests = await connection.QueryAsync<RequestsVM>(query);
                
                foreach (var _request in _requests)
                {
                    decimal totalCost = GetTotalCostByRequestId(_request.Id).Result;
                    _request.TotalCost = totalCost.ToString();
                    requests.Add(_request);
                }
            }

            return requests;
        }
        
        public async Task<decimal> GetTotalCostByRequestId(int requestId)
        {
            var query = RequestDetailQuery.GetRequestTotalCost();
            using(var connection = _dbContext.CreateConnection())
            {
                var totalCost = await connection.QueryAsync<decimal>(query, new { RequestId = requestId });
                return totalCost.First();
            }
        }

        public async Task <List<RemedialActionVM>> GetRemedialActions()
        {
            var query = RemedialActionQuery.GetRemedialActions();
            var remedialActions = new List<RemedialActionVM>();

            using (var connection = _dbContext.CreateConnection())
            {
                var _remedialActions = await connection.QueryAsync<RemedialActionVM>(query);

                foreach (var _remedialAction in _remedialActions)
                {
                    remedialActions.Add(_remedialAction);
                }
            }

            return remedialActions;

        }

        public async Task <List<UserAccountRequest>> GetAccountCreationRequests()
        {
            var query = RequestQuery.GetUserAccountRequests();
            var requests = new List<UserAccountRequest>();
            using (var connection = _dbContext.CreateConnection())
            {
                var _requests = await connection.QueryAsync<UserAccountRequest>(query);

                foreach (var _request in _requests)
                {
                    requests.Add(_request);
                }
            }

            return requests;
        }

        public async Task<Budget> GetBudget()
        {
            var query = RequestQuery.GetBudget();
            using (var connection = _dbContext.CreateConnection())
            {
                var budget = await connection.QueryAsync<Budget>(query);
                return budget.First();
            }
        }
    }
}
