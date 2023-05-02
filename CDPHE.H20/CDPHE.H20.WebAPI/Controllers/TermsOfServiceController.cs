using CDPHE.H20.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/termsofservice")]
    [ApiController]
    public class TermsOfServiceController : ControllerBase
    {
        public IConfiguration _configuration;
        private TOSService _tosService;

        public TermsOfServiceController(IConfiguration configuration)
        {
            _configuration = configuration;
            _tosService= new TOSService();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var TOS = await _tosService.GetTermsOfService();
            return Ok(TOS);
        }

        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] HtmlBody html)
        {
            var response = await _tosService.UpdateTermsOfService(html.Body);
            return Ok();
        }

        public class HtmlBody
        {
            public string Body { get; set; }
        }
    }
}
