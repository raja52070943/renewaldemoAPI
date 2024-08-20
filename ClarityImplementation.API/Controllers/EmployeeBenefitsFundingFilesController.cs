using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsFundingFilesController : Controller
    {
        private readonly GenericRepository<EmployeeBenefitsFundingFile> repository;
        public EmployeeBenefitsFundingFilesController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployeeBenefitsFundingFile>(context);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsFundingFile>>> GetEBFundingFileLists()
        {
            var ebFundingFileLists = await repository.GetAll();
            if (ebFundingFileLists == null)
            {
                return NotFound();
            }
            return Ok(ebFundingFileLists);
        }

        [HttpGet("ByEmployeeBenefitsFundingId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsFundingFile>> GetEBFundingFileListsByFundingId(int id)
        {
            var ebFundingFileLists = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsFundingId == id);
            if (ebFundingFileLists == null)
            {
                return NotFound();
            }
            return Ok(ebFundingFileLists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsFundingFile>> GetEBFundingFileList(int id)
        {
            var ebFundingFileList = await repository.GetById(id);

            if (ebFundingFileList == null)
            {
                return NotFound();
            }
            return Ok(ebFundingFileList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEBFundingFileList(int id, EmployeeBenefitsFundingFile ebFundingFileList)
        {
            if (id != ebFundingFileList.Id)
            {
                return BadRequest();
            }

            var updatedEBFundingFileList = await repository.Update(ebFundingFileList, id);
            if (updatedEBFundingFileList == null)
            {
                return NotFound();
            }
            return Ok(updatedEBFundingFileList);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeBenefitsFundingFile>> PostEBFundingFileList(EmployeeBenefitsFundingFile entity)
        {

            var addedEmployeeBenefitsFundingFile = await repository.Add(entity);
            if (addedEmployeeBenefitsFundingFile == null)
            {
                return NotFound();
            }
            return Ok(addedEmployeeBenefitsFundingFile);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEBFundingFileList(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
