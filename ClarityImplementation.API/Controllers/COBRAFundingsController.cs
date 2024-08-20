using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COBRAFundingsController : ControllerBase
    {
        private readonly GenericRepository<COBRAFunding> repository;
        private readonly GenericRepository<COBRAFundingFile> cobraFundingFileRepository;


        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public COBRAFundingsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<COBRAFunding>(context);
            cobraFundingFileRepository = new GenericRepository<COBRAFundingFile>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/COBRAFundings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAFunding>>> GetCOBRAFundings()
        {
            var COBRAFundings = await repository.GetAll();
            if (COBRAFundings == null)
            {
                return NotFound();
            }
            return Ok(COBRAFundings);
        }

        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<COBRAFunding>> GetCompanyCOBRAFunding(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");
            var cobraFunding = await repository.GetByCompanyId(entity => entity.CompanyId == id);
            if (cobraFunding == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync(baseURL + "/COBRAFundingFiles/ByCOBRAFundingId/" + cobraFunding.Id + "/" + cobraFunding.COBRAPremiumProvider);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var cobraFundingFile = JsonConvert.DeserializeObject<List<COBRAFundingFile>>(apiResponse);
                if (cobraFundingFile != null)
                {
                    cobraFunding.COBRAFundingFiles = cobraFundingFile;
                }
                else
                {
                    cobraFunding.COBRAFundingFiles = new List<COBRAFundingFile>();
                }

            }
            return Ok(cobraFunding);
        }


        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<COBRAFunding>> GetCOBRAFunding(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");
            var cobraFunding = await repository.GetById(id);
            if (cobraFunding == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync(baseURL + "/COBRAFundingFiles/ByCOBRAFundingId/" + cobraFunding.Id + "/" + cobraFunding.COBRAPremiumProvider);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var cobraFundingFile = JsonConvert.DeserializeObject<List<COBRAFundingFile>>(apiResponse);
                if (cobraFundingFile != null)
                {
                    cobraFunding.COBRAFundingFiles = cobraFundingFile;
                }
                else
                {
                    cobraFunding.COBRAFundingFiles = new List<COBRAFundingFile>();
                }

            }
            return Ok(cobraFunding);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRAFunding(int id, COBRAFunding cobraFunding)
        {
            if (id != cobraFunding.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAFunding = await repository.Update(cobraFunding, id);
            if (updatedCOBRAFunding == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAFunding);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostCOBRAFunding(COBRAFunding cobraFunding)
        {

            var addedCOBRAFunding = await repository.Add(cobraFunding);
            if (addedCOBRAFunding == null)
            {
                return NotFound();
            }
            COBRAFundingFile cobraFundingFile = new COBRAFundingFile();
            cobraFundingFile.CobraFundingId = addedCOBRAFunding.Id;
            await cobraFundingFileRepository.Add(cobraFundingFile);

            return RedirectToAction("GetCOBRAFunding", new { id = addedCOBRAFunding.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAFunding(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

        [HttpGet("CobraFundingProgress/{id}")]
        public async Task<ActionResult<CaseProgress>> GetCobraFundingProgress(int id)
        {
            var cobraFunding = await repository.GetById(id);
            if (cobraFunding == null)
            {
                return NotFound();
            }
            return Ok(new CaseProgress { Progress = cobraFunding.Progress });
        }
    }
}
