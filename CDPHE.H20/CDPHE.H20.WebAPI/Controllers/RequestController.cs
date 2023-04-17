using CDPHE.H20.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        // GET: api/<RequestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RequestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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


//SELECT Request.Id, Facility.Name, Facility.Address1, Facility.Address2, Facility.City, Facility.State, Facility.ZipCode, Facility.WQCID, [User].FirstName, [User].LastName, RequestDetail.Id AS RequestDetailId, RequestDetail.SampleName,
//                         RequestDetail.InitialSampleDate, RequestDetail.SampleResultOperator, RequestDetail.InitialSampleResult, RequestDetail.FlushSampleDate, RequestDetail.FlushResultOperator, RequestDetail.FlushSampleResult,
//                         RequestDetail.RemedialActionId, RequestDetail.MaterialCost, RequestDetail.MaterialLabor, Request.CreatedAt
//FROM            Request LEFT OUTER JOIN
//                         Facility ON Request.FacilityId = Facility.Id LEFT OUTER JOIN
//                         [User] ON Request.Id = [User].id LEFT OUTER JOIN
//                         RequestDetail ON Request.Id = RequestDetail.RequestId
