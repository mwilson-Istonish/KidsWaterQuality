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

namespace CDPHE.H20.Services
{
    public interface IRequestService
    {
        public Task<string> GetRequestAndDetails(int id);
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
                    requestAndDetails.Provider = _request.Provider;

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

        
    }
}
