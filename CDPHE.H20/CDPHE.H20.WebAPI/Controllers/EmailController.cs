using Amazon.Runtime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CDPHE.H20.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        public IConfiguration _configuration;
        private EmailService _emailService;

        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;
            _emailService = new EmailService();
        }

        [HttpPost]
        [Route("newplan/{requestid}")]
        public async Task<IActionResult> SendEmailForNewPlan(int requestId)
        {
            await _emailService.SendEmailForNewPlan(requestId);
            return Ok();
        }

        [HttpPost]
        [Route("planaccepted/{requestid}")]
        public async Task<IActionResult> SendEmailForPlanAccepted(int requestId)
        {
            await _emailService.SendEmailForPlanAccepted(requestId);
            return Ok();
        }

        [HttpPost]
        [Route("modsrequested/{requestid}")]
        public async Task<IActionResult> SendEmailForModsRequested(int requestId)
        {
            await _emailService.SendEmailForModificationsRequested(requestId);
            return Ok();
        }

        [HttpPost]
        [Route("planresent/{requestid}")]
        public async Task<IActionResult> SendEmailForPlanResent(int requestId)
        {
            await _emailService.SendEmailForPlanResent(requestId);
            return Ok();
        }

        [HttpPost]
        [Route("planapproved/{requestid}")]
        public async Task<IActionResult> SendEmailForPlanApproved(int requestId)
        {
            await _emailService.SendEmailForPlanApproved(requestId);
            return Ok();
        }

        [HttpPost]
        [Route("plancompleted/{requestid}")]
        public async Task<IActionResult> SendEmailForPlanCompleted(int requestId)
        {
            await _emailService.SendEmailForPlanCompleted(requestId);
            return Ok();
        }

        [HttpPost]
        [Route("planincomplete/{requestid}")]
        public async Task<IActionResult> SendEmailForPlanIncomplete(int requestId)
        {
            await _emailService.SendEmailForPlanInComplete(requestId);
            return Ok();
        }

        [HttpPost]
        [Route("planpaid/{requestid}")]
        public async Task<IActionResult> SendEmailForPlanPaid(int requestId)
        {
            await _emailService.SendEmailForPlanPaid(requestId);
            return Ok();
        }
    }
}
