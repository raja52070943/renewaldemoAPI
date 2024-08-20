using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobraFileUploadsController : Controller
    {
        private readonly GenericRepository<CobraFileUpload> repository;
        public CobraFileUploadsController(ClarityDbContext context)
        {
            repository = new GenericRepository<CobraFileUpload>(context);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CobraFileUpload>>> GetCobraFileUploads()
        {
            var cobraFileUploads = await repository.GetAll();
            if (cobraFileUploads == null)
            {
                return NotFound();
            }
            return Ok(cobraFileUploads);
        }

        [HttpGet("ByCobraBenefitId/{id}")]
        public async Task<ActionResult<CobraFileUpload>> GetAllUploadsByCobraBenefit(int id)
        {
            var cobraUploads = await repository.GetAllByCompanyId(entity => entity.CobraBenefitId == id);
            if (cobraUploads == null)
            {
                return NotFound();
            }
            return Ok(cobraUploads);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CobraFileUpload>> GetCobraFileUpload(int id)
        {
            var cobraFileUpload = await repository.GetById(id);

            if (cobraFileUpload == null)
            {
                return NotFound();
            }
            return Ok(cobraFileUpload);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCobraFileUpload(int id, CobraFileUpload cobraFileUpload)
        {
            if (id != cobraFileUpload.Id)
            {
                return BadRequest();
            }

            var updatedCobraFileUpload = await repository.Update(cobraFileUpload, id);
            if (updatedCobraFileUpload == null)
            {
                return NotFound();
            }
            return Ok(updatedCobraFileUpload);
        }

        [HttpPost]
        public async Task<ActionResult<CobraFileUpload>> PostCobraFileUpload (CobraFileUpload cobraFileUpload)
        {

            var addedCobraFileUpload = await repository.Add(cobraFileUpload);
            if (addedCobraFileUpload == null)
            {
                return NotFound();
            }
            return Ok(addedCobraFileUpload);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCobraFileUpload(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }

}
