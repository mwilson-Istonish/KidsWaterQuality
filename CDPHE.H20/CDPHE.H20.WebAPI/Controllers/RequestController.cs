using CDPHE.H20.Data.ViewModels;
using CDPHE.H20.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/request")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        public IConfiguration _configuration;
        private RequestService _requestService;

        public RequestController(IConfiguration configuration)
        {
            _configuration = configuration;
            _requestService= new RequestService();
        }

        // GET: api/<RequestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RequestController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = await _requestService.GetRequestAndDetails(id);
            return Ok(request);
        }

        [HttpGet("provider/{id}")]
        public async Task<IActionResult> GetByProviderId(int id)
        {
            var request = await _requestService.GetRequestsByProvider(id);
            return Ok(request);
        }

        [HttpGet("getallrequests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var requests = await _requestService.GetAllRequests();
            return Ok(requests);
        }

        [HttpGet("remedialactions")]
        public async Task<IActionResult> GetRemedialActions()
        {
            var remedialActions = await _requestService.GetRemedialActions();
            return Ok(remedialActions);
        }

        [HttpGet("accountcreationrequests")]
        public async Task<IActionResult> GetAccountCreationRequests()
        {
            var accountCreationRequests = await _requestService.GetAccountCreationRequests();
            return Ok(accountCreationRequests);
        }

        [HttpPost("uploadw9")]
        public async Task<IActionResult> UploadW9([FromBody]FileDataVM fileData)
        {
            var base64 = fileData.Base64Data;
            return Ok();
        }

        [HttpGet("budget")]
        public async Task<IActionResult> GetBudget()
        {
            var budget = await _requestService.GetBudget();
            return Ok(budget);
        }

        // POST api/<RequestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RequestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}