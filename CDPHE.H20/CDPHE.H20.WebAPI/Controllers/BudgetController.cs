using CDPHE.H20.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/budget")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        public IConfiguration _configuration;
        private BudgetService _budgetService;

        public BudgetController(IConfiguration configuration)
        {
            _configuration = configuration;
            _budgetService = new BudgetService();
        }

        // GET: api/<BudgetController>
        [HttpGet] 
        public async Task<IActionResult> Get()
        {
            var budget = await _budgetService.GetBudget();

            return Ok(budget);
        }

       
    }
}
