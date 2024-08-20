using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobraBenefitsController : Controller
    {
        private readonly GenericRepository<CobraBenefit> repository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CobraBenefitsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<CobraBenefit>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/CobraBenefits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CobraBenefit>>> GetCobraBenefits()
        {
            var cobraBenefits = await repository.GetAll();
            if (cobraBenefits == null)
            {
                return NotFound();
            }
            return Ok(cobraBenefits);
        }

        // GETById: api/CobraBenefits/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CobraBenefit>> GetCobraBenefit(int id)
        {
            var cobraBenefit = await repository.GetById(id);
            if (cobraBenefit == null)
            {
                return NotFound();
            }
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var response = await _httpClient.GetAsync(baseURL + "/CobraFileUploads/ByCobraBenefitId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var allFileUploads = JsonConvert.DeserializeObject<List<CobraFileUpload>>(apiResponse);
                if (allFileUploads != null)
                {
                    cobraBenefit.CobraFileUploads = allFileUploads;
                }
                else
                {
                    cobraBenefit.CobraFileUploads = new List<CobraFileUpload>();
                }
            }
            return Ok(cobraBenefit);
        }

        // GET: api/CobraBenefit/ByFileId/5
        [HttpGet("ByFileId/{id}")]
        public async Task<ActionResult<IEnumerable<CobraBenefit>>> GetCobraBenefits(int id)
        {
            var cobraBenefits = await repository.GetAllByCompanyId(entity => entity.FileId == id);
            if (cobraBenefits == null)
            {
                return NotFound();
            }
            var baseURL = _configuration.GetValue<string>("BaseURL");
            foreach (var cobraBenefit in cobraBenefits)
            {
                var response = await _httpClient.GetAsync(baseURL + "/CobraFileUploads/ByCobraBenefitId/" + cobraBenefit.Id);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var allFileUploads = JsonConvert.DeserializeObject<List<CobraFileUpload>>(apiResponse);
                    if (allFileUploads != null)
                    {
                        cobraBenefit.CobraFileUploads = allFileUploads;
                    }
                    else
                    {
                        cobraBenefit.CobraFileUploads = new List<CobraFileUpload>();
                    }
                }
            }
        
            return Ok(cobraBenefits);
        }

        // PUT: api/CobraBenefits/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCobraBenefit(int id, CobraBenefit cobraBenefit)
        {
            if (id != cobraBenefit.Id)
            {
                return BadRequest();
            }

            var updatedCobraBenefit = await repository.Update(cobraBenefit, id);
            if (updatedCobraBenefit == null)
            {
                return NotFound();
            }
            return Ok(updatedCobraBenefit);
        }

        // POST: api/CobraBenefits
        [HttpPost]
        public async Task<ActionResult<CobraBenefit>> PostCobraBenefit(CobraBenefit cobraBenefit)
        {

            var addedCobraBenefit = await repository.Add(cobraBenefit);
            if (addedCobraBenefit == null)
            {
                return NotFound();
            }
            return Ok(addedCobraBenefit);
        }

        // DELETE: api/CobraBenefits/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCobraBenefit(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
