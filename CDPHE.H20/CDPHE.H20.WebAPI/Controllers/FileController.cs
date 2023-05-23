using CDPHE.H20.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public IConfiguration _configuration;
        private FileService _fileService;

        public FileController(IConfiguration configuration)
        {
            _configuration = configuration;
            _fileService = new FileService();
        }

        // GET api/<FileController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilesByRequestId(int id)
        {
            var fileList = await _fileService.GetFiles(id);
            return Ok(fileList);
        }

        // POST api/<FileController>
        [HttpPost("filetype/{fileType}/requestId/{requestId}/ext/{extension}")]
        public async Task<IActionResult> AddFile([FromBody] string fileData, string fileType, string extension, int requestId)
        {
            var newFile = await _fileService.AddFile(fileData, fileType, extension, requestId);
            return Ok(newFile);
        }

        // PUT api/<FileController>/5
        [HttpPost("{folderName}")]
        public async Task<IActionResult> AddFolder(string folderName)
        {
            var folder = _fileService.AddFolder(folderName);
            return Ok(folder);
        }

        // DELETE api/<FileController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
