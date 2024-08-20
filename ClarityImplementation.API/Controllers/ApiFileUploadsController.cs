using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiFileUploadsController : Controller
    {
        private readonly GenericRepository<Api_File_Upload> repository;


        public ApiFileUploadsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<Api_File_Upload>(context);

        }

        // GET: api/FileUploads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Api_File_Upload>>> GetFileUploads()
        {
            var fileUploads = await repository.GetAll();
            if (fileUploads == null)
            {
                return NotFound();
            }
           
            return Ok(fileUploads);
        }

        // PUT: api/FileUpload/5

        [HttpPut("{fileUploadId}")]
        public async Task<IActionResult> PutFileFileUpload(int fileUploadId, Api_File_Upload fileUpload)
        {
            if (fileUploadId != fileUpload.FileUploadId)
            {
                return BadRequest();
            }

            var updatedFileUpload = await repository.Update(fileUpload, fileUploadId);
            if (updatedFileUpload == null)
            {
                return NotFound();
            }
            return Ok(updatedFileUpload);
        }

        // POST: api/FileUpload
        [HttpPost]
        public async Task<ActionResult<Api_File_Upload>> PostFileUpload(Api_File_Upload fileUpload)
        {


            var addedFileUpload = await repository.Add(fileUpload);
            if (addedFileUpload == null)
            {
                return NotFound();
            }
            return Ok(addedFileUpload);
        }
    }
}
