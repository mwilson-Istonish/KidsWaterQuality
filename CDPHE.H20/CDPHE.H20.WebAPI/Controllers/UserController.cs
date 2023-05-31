using CDPHE.H20.Data.Models;
using CDPHE.H20.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        private UserService _userService;

        public UserController(IConfiguration configuration) 
        {
            _configuration = configuration;
            _userService = new UserService();
        }
       
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsers()
        {
            var Users = await _userService.GetAllUsers();
            return Ok(Users);
        }

        [HttpGet("{id}")]
        // This method is an HTTP GET endpoint that takes an ID parameter from the route and returns a user with that ID
        public async Task<IActionResult> Get(int id)
        {
            var User = await _userService.GetUserById(id); // gets the user with the specified ID using the UserService
            return Ok(User); // returns an HTTP 200 OK response with the user as the response body
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

        // This method is an HTTP POST endpoint that sends a userGUID and a unique signin Token and returns a JWT or 401 if unauthorized"
        [HttpPost]
        [Route("login/{email}/{token}")]
        public async Task<IActionResult> Login(string email, string token)
        {
            var userRole = await _userService.Login(email, token);
            if(userRole != null)
            {
                
                TokenController tokenController = new TokenController(_configuration);
                var jwt = await tokenController.GetToken(userRole);
                
                // Returns an HTTP 200 response with the JWT token
                return Ok(jwt);
            }
            else
            {
                // Returns an HTTP 401 response if the user is not valid
                return Unauthorized();
            }
        }

        //HTTP POST endpoint that creates an account creation request if one is not already present
        [HttpPost]
        [Route("requestaccount/{firstName}/{lastName}/{email}")]
        public async Task<IActionResult> RequestAccount(string firstName, string lastName, string email)
        {
            var userAccountRequest = new UserAccountRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                RequestDate = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            await _userService.AddUserAccountRequest(userAccountRequest);
            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpGet("profile/{wqcid}")]
        public async Task<IActionResult> GetProfile(string wqcid)
        {
            var profile = await _userService.GetProfile(wqcid);
            return Ok(profile);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

    }
}
