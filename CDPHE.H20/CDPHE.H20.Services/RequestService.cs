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
using System.Diagnostics.Tracing;

namespace CDPHE.H20.Services
{
    public interface IRequestService
    {
        public Task<string> GetRequestAndDetails(int id);
        public Task<List<RequestsVM>> GetRequestsByProvider(int id);
        public Task<List<RemedialActionVM>> GetRemedialActions();
        public Task<List<UserAccountRequest>> GetAccountCreationRequests();
        public Task<Budget> GetBudget();
        public Task<int> AddRequest(RequestAndDetails requestAndDetails, int userId);
        public Task<int> AddRequestDetail(int userId, int requesetId, ReqDetails reqDetails);
        public Task<string> DeleteRequestDetail(int id, int userId);
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
                    requestAndDetails.FacilityId = _request.FacilityId;
                    requestAndDetails.FacilityName = _request.FacilityName;
                    requestAndDetails.Address1 = _request.Address1;
                    if (requestAndDetails.Address2 != null)
                    {
                        requestAndDetails.Address2 = _request.Address2;
                    }
                    if (requestAndDetails.Address3 != null)
                    {
                        requestAndDetails.Address3 = _request.Address3;
                    }
                    requestAndDetails.City= _request.City;
                    requestAndDetails.State = _request.State;
                    requestAndDetails.ZipCode = _request.ZipCode;
                    requestAndDetails.WQCID= _request.WQCID;
                    requestAndDetails.Provider = _request.Provider;
                    requestAndDetails.Email = _request.Email;
                    requestAndDetails.Phone = _request.Phone;

                    requestAndDetails.Details = new List<ReqDetails>();

                    foreach (var _details in _requestDetails)
                    {
                        _details.RequestId = _request.Id;
                        requestAndDetails.Details.Add(_details);
                        requestAndDetails.TotalCostMaterials = (decimal)(_details.ActualMaterialCost + requestAndDetails.TotalCostMaterials);
                        requestAndDetails.TotalCostLabor = (decimal)(_details.ActualLaborCost + requestAndDetails.TotalCostLabor);
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

        public async Task<int> AddRequest(RequestAndDetails requestAndDetails, int userId)
        {
            // returns id of new request
            var query = RequestQuery.InsertNewRequest();
            using (var connection = _dbContext.CreateConnection())
            {
                var id = connection.Query<int>(query, new
                {
                    UserId = userId,
                    FacilityId = requestAndDetails.FacilityId,
                    Status = "New",
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    LastUpdated = DateTime.Now,
                    IsActive = true
                });
                
                foreach(var detail in requestAndDetails.Details)
                {
                    var detailId = await AddRequestDetail(userId, id.First(), detail);
                }

                return id.First();
            }
        }

        public async Task<int> AddRequestDetail(int userId, int requestId, ReqDetails reqDetails)
        {
            // returns id of new request detail
            var query = RequestDetailQuery.InsertNewRequestDetail();
            using (var connection = _dbContext.CreateConnection())
            {
                var id = connection.Query<int>(query, new
                {
                    RequestId = requestId,
                    SampleName = reqDetails.SampleName,
                    InitialSampleDate = reqDetails.InitialSampleDate,
                    SampleResultOperator = reqDetails.SampleResultOperator,
                    InitialSampleResult = reqDetails.InitialSampleResult,
                    FlushSampleDate = reqDetails.FlushSampleDate,
                    FlushResultOperator = reqDetails.FlushResultOperator,
                    FlushSampleResult = reqDetails.FlushSampleResult,
                    RemedialActionId = reqDetails.RemedialActionId,
                    ExpectedMaterialCost = reqDetails.ExpectedMaterialCost,
                    ExpectedLaborCost = reqDetails.ExpectedLaborCost,
                    // Values for Actual intentionally set to Expected for ease of totalling costs
                    ActualMaterialCost = reqDetails.ExpectedMaterialCost,
                    ActualLaborCost = reqDetails.ExpectedLaborCost,
                    ConfirmationSampleResultDate = DateTime.Parse("1900-01-01"),
                    ConfirmationSampleResultOperator = reqDetails.ConfirmationSampleResultOperator,
                    ConfirmationSampleResult = reqDetails.ConfirmationSampleResult,
                    InHouseLabor = reqDetails.InHouseLabor,
                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedBy = userId,
                    LastUpdated = DateTime.Now,
                    IsActive = 1
                });

                return id.First();
            }
        }

        public async Task<string> DeleteRequestDetail(int id, int userId)
        {
            var query = RequestDetailQuery.DeleteRequestDetail();
            using (var connection = _dbContext.CreateConnection())
            {
                await connection.QueryAsync(query, new { Id = id, UserId = userId, Now = DateTime.Now });
            }

            return "{ Success }";
        }
    }
}
