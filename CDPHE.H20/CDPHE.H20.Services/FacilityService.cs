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
using System.Net.Http;
using System.Security.Claims;

namespace CDPHE.H20.Services
{
    public interface IFacilityService
    {
        public Task<FacilityDetails> GetFacilityByWQCID(string wqcid);
        public Task<int> AddFacility(FacilityDetails facility, int userId);
        public Task<Facility> GetFacilityById(int id);
    }

    public class FacilityService : IFacilityService
    {
        private readonly DapperContext _dbContext = new DapperContext();
        private readonly DapperMySQLContext _dbMySQLContext = new DapperMySQLContext();

        public async Task<int> AddFacility(FacilityDetails facility, int userId)
        {
            var query = FacilityQuery.InsertFacility();
            using (var connection = _dbContext.CreateConnection())
            {
                var facilityId = await connection.QuerySingleAsync<int>(query, new
                {
                    WQCID = facility.WQCID,
                    Name = facility.Name,
                    Address1 = facility.Address1,
                    Address2 = facility.Address2,
                    Address3 = facility.Address3,
                    City = facility.City,
                    State = facility.State,
                    ZipCode = facility.ZipCode,
                    County = facility.County,
                    CreatedAt = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    IsActive = 1
                });
                return facilityId;
            }
        }

        public async Task<Facility> GetFacilityById(int id)
        {
            var query = FacilityQuery.GetFacilityById();
            using (var connection = _dbContext.CreateConnection())
            {
                var facility = await connection.QueryAsync<Facility>(query, new { Id = id });
                return facility.First();
            }
        }

        public async Task<FacilityDetails> GetFacilityByWQCID(string wqcid)
        {
            // Querying MySQL DB to get Facility details and add them to SQL server
            var query = FacilityQuery.GetFacilityByWQCID();
            using (var connection = _dbMySQLContext.CreateConnection())
            {
                var facility = await connection.QueryAsync<FacilityDetails>(query, new { WQCID = wqcid });
                return facility.First();
            }
        }
    }
}
