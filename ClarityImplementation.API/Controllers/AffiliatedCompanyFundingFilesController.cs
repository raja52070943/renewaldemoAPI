using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffiliatedCompanyFundingFilesController : Controller
    {
        private readonly GenericRepository<AffiliatedCompanyFundingFile> repository;
        public AffiliatedCompanyFundingFilesController(ClarityDbContext context)
        {
            repository = new GenericRepository<AffiliatedCompanyFundingFile>(context);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AffiliatedCompanyFundingFile>>> GetAffiliatedCompanyFundingFiles()
        {
            var all = await repository.GetAll();
            if (all == null)
            {
                return NotFound();
            }
            return Ok(all);
        }

        [HttpGet("ByAffiliatedCompanyId/{id}")]
        public async Task<ActionResult<AffiliatedCompanyFundingFile>> GetAffiliatedCompanyFundingFileByCompany(int id)
        {
            var allByCompany = await repository.GetAllByCompanyId(entity => entity.AffiliatedCompanyId == id);
            if (allByCompany == null)
            {
                return NotFound();
            }
            return Ok(allByCompany);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AffiliatedCompanyFundingFile>> GetAffiliatedCompanyFundingFile(int id)
        {
            var affiliatedCompanyFundingFile = await repository.GetById(id);

            if (affiliatedCompanyFundingFile == null)
            {
                return NotFound();
            }
            return Ok(affiliatedCompanyFundingFile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAffiliatedCompanyFundingFile(int id, AffiliatedCompanyFundingFile affiliatedCompanyFundingFile)
        {
            if (id != affiliatedCompanyFundingFile.Id)
            {
                return BadRequest();
            }

            var updatedAffiliatedCompanyFundingFile = await repository.Update(affiliatedCompanyFundingFile, id);
            if (updatedAffiliatedCompanyFundingFile == null)
            {
                return NotFound();
            }
            return Ok(updatedAffiliatedCompanyFundingFile);
        }

        [HttpPost]
        public async Task<ActionResult<AffiliatedCompanyFundingFile>> PostAffiliatedCompanyFundingFile(AffiliatedCompanyFundingFile entity)
        {

            var addedAffiliatedCompanyFundingFile = await repository.Add(entity);
            if (addedAffiliatedCompanyFundingFile == null)
            {
                return NotFound();
            }
            return Ok(addedAffiliatedCompanyFundingFile);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAffiliatedCompanyFundingFile(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
