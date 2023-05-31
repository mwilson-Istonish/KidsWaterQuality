using CDPHE.H20.Data.Context;
using CDPHE.H20.Data.Models;
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
    public interface IRoleService
    {
        public Task<List<Role>> GetRoles();
    }

    public class RoleService
    {
        private readonly DapperContext _dbContext = new DapperContext();
        public RoleService() { }

        public async Task<List<Role>> GetRoles()
        {
            var query = RoleQuery.GetAllRoles();
            var roles = new List<Role>();
            using (var connection = _dbContext.CreateConnection())
            {
                var _roles = await connection.QueryAsync<Role>(query);

                foreach (var _role in _roles)
                {
                    roles.Add(_role);
                }
            }

            return roles;
        }

    }
}
