﻿using CDPHE.H20.Data.Models;
using CDPHE.H20.Data.ViewModels;
using CDPHE.H20.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/requestdetail")]
    [ApiController]
    public class RequestDetailController : ControllerBase
    {
        public IConfiguration _configuration;
        private RequestService _requestService;

        public RequestDetailController(IConfiguration configuration)
        {
            _configuration = configuration;
            _requestService = new RequestService();
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddRequestDetail(int requestId, ReqDetails reqDetail)
        {
            // Returns Id of new RequestDetail
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = Convert.ToInt32(identity.FindFirst("Id").Value);
            var requestDetail = await _requestService.AddRequestDetail(id, requestId, reqDetail);
            return Ok(requestDetail);
        }

        //[HttpPut]
        //[Route("update/{id}")]
        //public async Task<IActionResult> Update(ReqDetails reqDetail)
        //{
        //    //var requestDetail = await _requestService.UpdateRequestDetail(reqDetail);
        //    //return Ok(requestDetail);
        //}

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Convert.ToInt32(identity.FindFirst("Id").Value);
            var deleteRequest = await _requestService.DeleteRequestDetail(id, userId);
            return Ok(deleteRequest);
        }
    }
}