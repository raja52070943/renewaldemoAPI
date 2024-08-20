using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Event;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendlyEventsController : ControllerBase
    {
        private readonly GenericRepository<CalendlyEvent> repository;

        public CalendlyEventsController(ClarityDbContext context)
        {
            repository = new GenericRepository<CalendlyEvent>(context);
            
        }

        [HttpPost]
        public async Task<ActionResult<CalendlyEvent>> PostCalendlyEvent(CalendlyEvent calendlyEvent)
        {
            var addedCalendlyEvent = await repository.Add(calendlyEvent);
            if (addedCalendlyEvent == null)
            {
                return NotFound();
            }
            
            return Ok(addedCalendlyEvent);
        }


        [HttpGet("GetCalendlyEventByCompanyId/{id}")]
        public async Task<ActionResult<CalendlyEvent>> GetCalendlyEventByCompanyId(int id)
        {
            var calendlyEvent = await repository.GetByCompanyId(entity => entity.CompanyId == id);
            if (calendlyEvent == null)
            {
                return NotFound();
            }
           
            return Ok(calendlyEvent);
        }
    }
}
