using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        // POST api/<EmailController>
        [HttpPost]
        [Route("send")]
        public void SendEmail([FromBody] string value)
        {
        }
    }
}
