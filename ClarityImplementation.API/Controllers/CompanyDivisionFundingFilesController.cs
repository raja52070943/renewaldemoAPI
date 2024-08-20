using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyDivisionFundingFilesController : Controller
    {
        private readonly GenericRepository<CompanyDivisionFundingFile> repository;
        public CompanyDivisionFundingFilesController(ClarityDbContext context)
        {
            repository = new GenericRepository<CompanyDivisionFundingFile>(context);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDivisionFundingFile>>> GetCompanyDivisionFundingFiles()
        {
            var companyDivisionFundingFiles = await repository.GetAll();
            if (companyDivisionFundingFiles == null)
            {
                return NotFound();
            }
            return Ok(companyDivisionFundingFiles);
        }

        [HttpGet("ByCompanyDivisionId/{id}")]
        public async Task<ActionResult<CompanyDivisionFundingFile>> GetCompanyDivisionFundingFileByCompanyDivision(int id)
        {
            var GetAllFundingFilesByCompanyDivision = await repository.GetAllByCompanyId(entity => entity.CompanyDivisionId == id);
            if (GetAllFundingFilesByCompanyDivision == null)
            {
                return NotFound();
            }
            return Ok(GetAllFundingFilesByCompanyDivision);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDivisionFundingFile>> GetCompanyDivisionFundingFile(int id)
        {
            var companyDivisionFundingFile = await repository.GetById(id);

            if (companyDivisionFundingFile == null)
            {
                return NotFound();
            }
            return Ok(companyDivisionFundingFile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompanyDivisionFundingFile(int id, CompanyDivisionFundingFile companyDivisionFundingFile)
        {
            if (id != companyDivisionFundingFile.Id)
            {
                return BadRequest();
            }

            var updatedCompanyDivisionFundingFile = await repository.Update(companyDivisionFundingFile, id);
            if (updatedCompanyDivisionFundingFile == null)
            {
                return NotFound();
            }
            return Ok(updatedCompanyDivisionFundingFile);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDivisionFundingFile>> PostCompanyDivisionFundingFile(CompanyDivisionFundingFile entity)
        {

            var addedCompanyDivisionFundingFile = await repository.Add(entity);
            if (addedCompanyDivisionFundingFile == null)
            {
                return NotFound();
            }
            return Ok(addedCompanyDivisionFundingFile);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyDivisionFundingFile(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
