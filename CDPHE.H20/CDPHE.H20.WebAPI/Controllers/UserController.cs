using CDPHE.H20.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController() 
        {
            _userService = new UserService();
        }
        // private UserService _userService { get; }
        // GET: api/<UserController>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetUsers()
        {
            
            var Users = await _userService.GetAllUsers();
            return Ok(Users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost]
        [Route("sendemail/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            bool emailSent = await _userService.EmailUser(email);
            return Ok(emailSent);
        }

        [HttpPost]
        [Route("login/{userguid}/{token}")]
        public async Task<IActionResult> Login(string userguid, string token)
        {
            var jwt = await _userService.Login(userguid, token);
            return Ok(jwt);
        }

        [HttpPost]
        [Route("logout/{id}")]
        public void Logout(int Id)
        {

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
