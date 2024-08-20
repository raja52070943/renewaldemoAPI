using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCasesController : Controller
    {
        private readonly GenericRepository<Api_Case> repository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ApiCasesController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<Api_Case>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Api_Case>>> GetAllCases()
        {
            var apiCases = await repository.GetAll();
            if (apiCases == null)
            {
                return NotFound();
            }
            return Ok(apiCases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Api_Case>> GetApiCaseByCaseId(string id)
        {
            var apiCase = await repository.GetByCaseId(id);
            if (apiCase == null)
            {
                return NotFound();
            }
            return Ok(apiCase);
        }
    }
}
