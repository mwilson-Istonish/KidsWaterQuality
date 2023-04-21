﻿using CDPHE.H20.Services;
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

        [HttpPost]
        [Route("login/{userguid}/{token}")]
        public async Task<IActionResult> Login(string userguid, string token)
        {
            var userRole = await _userService.Login(userguid, token);
            if(userRole != null)
            {
                TokenController tokenController = new TokenController(_configuration);
                var jwt = await tokenController.GetToken(userRole);
                return Ok(jwt);
            }
            else
            {
                return Unauthorized();
            }
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
