using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsFileUploadItemsController : Controller
    {
        private readonly GenericRepository<EmployeeBenefitsFileUploadItem> repository;
        public EmployeeBenefitsFileUploadItemsController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployeeBenefitsFileUploadItem>(context);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsFileUploadItem>>> GetEmployeeBenefitsFileUploadItems()
        {
            var employeeBenefitsFileUploadItems = await repository.GetAll();
            if (employeeBenefitsFileUploadItems == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsFileUploadItems);
        }

        [HttpGet("ByEmployeeBenefitsFileUploadId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsFileUploadItem>> GetAllEmployeeBenefitsFileUploadItems(int id)
        {
            var employeeBenefitsFileUploadItemsById = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitId == id);
            if (employeeBenefitsFileUploadItemsById == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsFileUploadItemsById);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsFileUploadItem>> GetEmployeeBenefitsFileUploadItem(int id)
        {
            var employeeBenefitsFileUploadItem = await repository.GetById(id);

            if (employeeBenefitsFileUploadItem == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsFileUploadItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsFileUploadItem(int id, EmployeeBenefitsFileUploadItem employeeBenefitsFileUploadItem)
        {
            if (id != employeeBenefitsFileUploadItem.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsFileUploadItem = await repository.Update(employeeBenefitsFileUploadItem, id);
            if (updatedEmployeeBenefitsFileUploadItem == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsFileUploadItem);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeBenefitsFileUploadItem>> PostEmployeeBenefistFileUploadItem(EmployeeBenefitsFileUploadItem employeeBenefitsFileUploadItem)
        {

            var addedEmployeeBenefitsFileUploadItem = await repository.Add(employeeBenefitsFileUploadItem);
            if (addedEmployeeBenefitsFileUploadItem == null)
            {
                return NotFound();
            }
            //var existingEmployeBenefitsFunding = await employeeBenefitsFundingRepository.GetById(ebFundingFileList.EmployeeBenefitsFundingId);
            //await employeeBenefitsFundingRepository.Update(existingEmployeBenefitsFunding, ebFundingFileList.EmployeeBenefitsFundingId);
            return Ok(addedEmployeeBenefitsFileUploadItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsFileUploadItem(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
