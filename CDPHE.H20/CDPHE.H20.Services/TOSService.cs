using CDPHE.H20.Data.Context;
using CDPHE.H20.Data.Queries;
using CDPHE.H20.Data.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Services
{
    public interface ITOSService
    {
        public Task<string> GetTermsOfService();
        public Task<string> UpdateTermsOfService(string termsOfService);
    }

    public class TOSService : ITOSService
    {
        private readonly DapperContext _dbContext = new DapperContext();

        public TOSService()
        {

        }

        public async Task<string> GetTermsOfService()
        {
            string html = "";
            var query = TOSQuery.GetTermsOfService();
            using(var connection = _dbContext.CreateConnection())
            {
                var termsOfService = await connection.QueryAsync<string>(query);
                html = termsOfService.First().ToString();   
            }

            return html;
        }

        public async Task<string> UpdateTermsOfService(string termOfService)
        {
            string msg = "{ Success }";
            var query = TOSQuery.UpdateTermsOfService();

            using (var connection = _dbContext.CreateConnection())
            {
                var termsOfService = await connection.QueryFirstOrDefaultAsync<string>(query, new { Body = termOfService });
            }
            return msg;
        }
    }
}
