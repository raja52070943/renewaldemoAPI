using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COBRAFundingFilesController : Controller
    {
        private readonly GenericRepository<COBRAFundingFile> repository;
        public COBRAFundingFilesController(ClarityDbContext context)
        {
            repository = new GenericRepository<COBRAFundingFile>(context);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAFundingFile>>> GetCOBRAFundingFiles()
        {
            var cobraFundingFiles = await repository.GetAll();
            if (cobraFundingFiles == null)
            {
                return NotFound();
            }
            return Ok(cobraFundingFiles);
        }

        [HttpGet("ByCOBRAFundingId/{id}/{cobraPremiumProvider}")]
        public async Task<ActionResult<COBRAFundingFile>> GetCOBRAFundingFilesByFundingId(int id, string cobraPremiumProvider)
        {
            var cobraFundingFiles = await repository.GetAllByCompanyId(entity => entity.CobraFundingId == id && entity.CobraPremiumProvider == cobraPremiumProvider);
            if (cobraFundingFiles == null)
            {
                return NotFound();
            }
            return Ok(cobraFundingFiles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<COBRAFundingFile>> GetCOBRAFundingFile(int id)
        {
            var cobraFundingFile = await repository.GetById(id);

            if (cobraFundingFile == null)
            {
                return NotFound();
            }
            return Ok(cobraFundingFile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCOBRAFundingFile(int id, COBRAFundingFile cobraFundingFile)
        {
            if (id != cobraFundingFile.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAFundingFile = await repository.Update(cobraFundingFile, id);
            if (updatedCOBRAFundingFile == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAFundingFile);
        }

        [HttpPost]
        public async Task<ActionResult<COBRAFundingFile>> PostCOBRAFundingFile(COBRAFundingFile entity)
        {

            var addedCOBRAFundingFile = await repository.Add(entity);
            if (addedCOBRAFundingFile == null)
            {
                return NotFound();
            }
            return Ok(addedCOBRAFundingFile);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAFundingFile(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
