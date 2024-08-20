using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffiliatedCompanyDivisionFundingFilesController : Controller
    {
        private readonly GenericRepository<AffiliatedCompanyDivisionFundingFile> repository;
        public AffiliatedCompanyDivisionFundingFilesController(ClarityDbContext context)
        {
            repository = new GenericRepository<AffiliatedCompanyDivisionFundingFile>(context);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AffiliatedCompanyDivisionFundingFile>>> GetAffiliatedCompanyDivisionFundingFiles()
        {
            var all = await repository.GetAll();
            if (all == null)
            {
                return NotFound();
            }
            return Ok(all);
        }

        [HttpGet("ByAffiliatedCompanyDivisionId/{id}")]
        public async Task<ActionResult<AffiliatedCompanyDivisionFundingFile>> GetAffiliatedCompanyDivisionFundingFileByCompany(int id)
        {
            var allByCompanyDivision = await repository.GetAllByCompanyId(entity => entity.AffiliatedCompanyDivisionId == id);
            if (allByCompanyDivision == null)
            {
                return NotFound();
            }
            return Ok(allByCompanyDivision);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AffiliatedCompanyDivisionFundingFile>> GetAffiliatedCompanyDivisionFundingFile(int id)
        {
            var affiliatedCompanyDivisionFundingFile = await repository.GetById(id);

            if (affiliatedCompanyDivisionFundingFile == null)
            {
                return NotFound();
            }
            return Ok(affiliatedCompanyDivisionFundingFile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAffiliatedCompanyDivisionFundingFile(int id, AffiliatedCompanyDivisionFundingFile affiliatedCompanyDivisionFundingFile)
        {
            if (id != affiliatedCompanyDivisionFundingFile.Id)
            {
                return BadRequest();
            }

            var updatedAffiliatedCompanyDivisionFundingFile = await repository.Update(affiliatedCompanyDivisionFundingFile, id);
            if (updatedAffiliatedCompanyDivisionFundingFile == null)
            {
                return NotFound();
            }
            return Ok(updatedAffiliatedCompanyDivisionFundingFile);
        }

        [HttpPost]
        public async Task<ActionResult<AffiliatedCompanyDivisionFundingFile>> PostAffiliatedCompanyDivisionFundingFile(AffiliatedCompanyDivisionFundingFile entity)
        {

            var addedAffiliatedCompanyDivisionFundingFile = await repository.Add(entity);
            if (addedAffiliatedCompanyDivisionFundingFile == null)
            {
                return NotFound();
            }
            return Ok(addedAffiliatedCompanyDivisionFundingFile);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAffiliatedCompanyDivisionFundingFile(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
