using Azure.Core;
using CDPHE.H20.Data.Models;
using CDPHE.H20.Data.ViewModels;
using CDPHE.H20.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        public IConfiguration _configuration;
        private NoteService _noteService;

        public NoteController(IConfiguration configuration)
        {
            _configuration = configuration;
            _noteService = new NoteService();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNote(Note note)
        {
            // Returns Id of new Note
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = Convert.ToInt32(identity.FindFirst("Id").Value);
            var requestDetail = await _noteService.AddNote(id, note);
            return Ok(requestDetail);
        }

        // GET api/<NoteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotesByRequestId(int id)
        {
            var requestDetail = await _noteService.GetNotes(id);
            return Ok(requestDetail);
        }

        // POST api/<NoteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NoteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NoteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
