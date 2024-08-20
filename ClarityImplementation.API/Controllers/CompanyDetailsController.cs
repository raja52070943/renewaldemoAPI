using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyDetailsController : Controller
    {
        private readonly GenericRepository<CompanyDetails> repository;

        public CompanyDetailsController(ClarityDbContext context)
        {
            repository = new GenericRepository<CompanyDetails>(context);
        }

        // GET: api/CompanyDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDetails>>> GetCompanyDetails()
        {
            var companyDetails = await repository.GetAll();
            if (companyDetails == null)
            {
                return NotFound();
            }
            return Ok(companyDetails);
        }

        // GET: api/CompanyDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDetails>> CompanyDetail(int id)
        {
            var companyDetail = await repository.GetById(id);
            if (companyDetail == null)
            {
                return NotFound();
            }
            return Ok(companyDetail);
        }

        // GET: api/CompanyDetails/5
        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<Address>> GetCompanyDetailsByCompany(int id)
        {
            var companyDetails = await repository.GetByCompanyId(entity => entity.CompanyId == id);
            if (companyDetails == null)
            {
                return NotFound();
            }
            return Ok(companyDetails);
        }

        // PUT: api/CompanyDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyDetails(int id, CompanyDetails companyDetails)
        {
            if (id != companyDetails.Id)
            {
                return BadRequest();
            }

            var updatedCompanyDetails = await repository.Update(companyDetails, id);
            if (updatedCompanyDetails == null)
            {
                return NotFound();
            }
            return Ok(updatedCompanyDetails);
        }

        // POST: api/CompanyDetails

        [HttpPost]
        public async Task<ActionResult<CompanyDetails>> PostCompanyDetails(CompanyDetails companyDetails)
        {
            var addedCompanyDetails = await repository.Add(companyDetails);
            if (addedCompanyDetails == null)
            {
                return NotFound();
            }
            return Ok(addedCompanyDetails);
        }


        // DELETE: api/CompanyDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyDetails(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
