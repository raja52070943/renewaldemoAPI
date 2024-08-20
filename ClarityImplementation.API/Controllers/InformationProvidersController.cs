using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationProvidersController : Controller
    {
        private readonly GenericRepository<InformationProvider> repository;

        public InformationProvidersController(ClarityDbContext context)
        {
            repository = new GenericRepository<InformationProvider>(context);
        }

        // GET: api/InformationProviders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InformationProvider>>> GetInformationProviders()
        {
            var informationProviders = await repository.GetAll();
            if (informationProviders == null)
            {
                return NotFound();
            }
            return Ok(informationProviders);
        }

        // GET: api/InformationProviders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformationProvider>> GetInformationProvider(int id)
        {
            var informationProvider = await repository.GetById(id);
            if (informationProvider == null)
            {
                return NotFound();
            }
            return Ok(informationProvider);
        }

        // PUT: api/InformationProvider/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformationProvider(int id, InformationProvider informationProvider)
        {
            if (id != informationProvider.Id)
            {
                return BadRequest();
            }

            var updatedInformationProvider = await repository.Update(informationProvider, id);
            if (updatedInformationProvider == null)
            {
                return NotFound();
            }

            return Ok(updatedInformationProvider);
        }

        // POST: api/InformationProviders

        [HttpPost]
        public async Task<ActionResult<InformationProvider>> PostInformationProvider(InformationProvider informationProvider)
        {
            var addedInformationProvider = await repository.Add(informationProvider);
            if (addedInformationProvider == null)
            {
                return NotFound();
            }
            return Ok(addedInformationProvider);
        }

        // DELETE: api/InformationProviders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformationProvider(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
