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

namespace CDPHE.H20.Services
{
    public interface IBudgetService
    {
        public Task<BudgetVM> GetBudget();
    }

    public class BudgetService : IBudgetService
    {
        private readonly DapperContext _dbContext = new DapperContext();

        public async Task<BudgetVM> GetBudget()
        {
            var query = BudgetQuery.GetBudget();
            BudgetVM budgetVM = new BudgetVM();
            budgetVM.Annual = 10000;

            using (var connection = _dbContext.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query))
                {
                    var _details = await multi.ReadAsync<BudgetItems>();
                    var _budget = await multi.ReadFirstAsync<AnnualBudget>();

                    foreach(var _detail in _details)
                    {
                        switch (_detail.Status)
                        {
                            case "Complete":
                                budgetVM.Spent += _detail.ActualMaterialCost;
                                budgetVM.Spent += _detail.ActualLaborCost;
                                break;

                            case "New":
                                budgetVM.Requested += _detail.ActualMaterialCost;
                                budgetVM.Requested += _detail.ActualLaborCost;
                                break;

                            case "In Progress":
                                budgetVM.Approved += _detail.ActualMaterialCost;
                                budgetVM.Approved += _detail.ActualLaborCost;
                                break;
                        }
                        
                    }

                    budgetVM.Annual = _budget.DollarAmount;
                    budgetVM.Remaining = _budget.DollarAmount - budgetVM.Spent - budgetVM.Requested - budgetVM.Approved;
                }
            }

            return budgetVM;
        }
    }

}
