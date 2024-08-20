using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyStatusesController : ControllerBase
    {
        private readonly GenericRepository<CompanyStatus> repository;

        public CompanyStatusesController(ClarityDbContext context)
        {
            repository = new GenericRepository<CompanyStatus>(context);
        }
        // GET: api/CompanyStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyStatus>>> GetCompanyStatuses()
        {
            var companyStatuses = await repository.GetAll();
            if (companyStatuses == null)
            {
                return NotFound();
            }
            return Ok(companyStatuses);
        }

        // GET: api/CompanyStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyStatus>> GetCompanyStatus(int id)
        {
            var companyStatus = await repository.GetById(id);
            if (companyStatus == null)
            {
                return NotFound();
            }
            return Ok(companyStatus);
        }

        // GET: api/CompanyDetails/5
        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<CompanyStatus>> GetCompanyStatusByCompany(int id)
        {
            var companyStatus = await repository.GetByCompanyId(entity => entity.CompanyId == id);
            if (companyStatus == null)
            {
                return NotFound();
            }
            return Ok(companyStatus);
        }

        // PUT: api/CompanyStatuses/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyStatus(int id, CompanyStatus companyStatus)
        {
            if (id != companyStatus.Id)
            {
                return BadRequest();
            }

            var updatedCompanyStatus = await repository.Update(companyStatus, id);
            if (updatedCompanyStatus == null)
            {
                return NotFound();
            }
            return Ok(updatedCompanyStatus);
        }

        // POST: api/CompanyStatuses

        [HttpPost]
        public async Task<ActionResult<CompanyStatus>> PostCompanyStatus(CompanyStatus companyStatus)
        {
            var addedCompanyStatus = await repository.Add(companyStatus);
            if (addedCompanyStatus == null)
            {
                return NotFound();
            }
            return Ok(addedCompanyStatus);
        }

        // DELETE: api/CompanyStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyStatus(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

    }
}
