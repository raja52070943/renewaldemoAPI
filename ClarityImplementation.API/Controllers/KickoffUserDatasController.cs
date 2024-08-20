using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Kickoff;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KickoffUserDatasController : Controller
    {
        private readonly GenericRepository<KickoffUserData> repository;

        public KickoffUserDatasController(ClarityDbContext context)
        {
            repository = new GenericRepository<KickoffUserData>(context);
        }

        // GET: api/KickoffUserData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KickoffUserData>>> GetAllUserData()
        {
            var allUserData = await repository.GetAll();
            if (allUserData == null)
            {
                return NotFound();
            }
            return Ok(allUserData);
        }

        // GET: api/KickoffUserData
        [HttpGet("ByEmail/{email}")]
        public async Task<ActionResult<KickoffUserData>> GetKickOffDataByEmail(string email)
        {
            var caseOwnerData = await repository.GetByCompanyId(entity => entity.Email == email);
            if (caseOwnerData == null)
            {
                return NotFound();
            }

            return Ok(caseOwnerData);
        }

        [HttpGet("GetCalendlyUrlByEmail/{email}")]
        public async Task<ActionResult<KickoffUserData>> GetCalendlyUrlByEmail(string email)
        {
            var caseOwnerData = await repository.GetByCompanyId(entity => entity.Email == email);
            if (caseOwnerData == null)
            {
                return NotFound();
            }
            string calendlyUrl = caseOwnerData.CalendlyUrl;
            return Ok(calendlyUrl);
        }

    }
}
