using CDPHE.H20.Data.ViewModels;
using CDPHE.H20.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;


namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        public IConfiguration _configuration;
        private StatusService _statusService;

        public StatusController(IConfiguration configuration)
        {
            _configuration = configuration;
            _statusService = new StatusService();
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> Get(string status)
        {
            var response = await _statusService.GetRequestsByStatus(status);
            return Ok(response);
        }

        [HttpPost("{id}/{newStatus}")]
        public async Task<IActionResult> UpdateStatus(int id, string newStatus)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(identity.FindFirst("Id").Value);
            var response = await _statusService.UpdateStatus(id, userId, newStatus);
            return Ok(response);
        }

    }
}
