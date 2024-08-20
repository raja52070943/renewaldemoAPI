using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffiliatedCompanyDivisionsController : ControllerBase
    {
        private readonly GenericRepository<AffiliatedCompanyDivision> repository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly GenericRepository<AffiliatedCompany> affiliatedCompanyRepository;

        public AffiliatedCompanyDivisionsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<AffiliatedCompanyDivision>(context);
            affiliatedCompanyRepository = new GenericRepository<AffiliatedCompany>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/AffiliatedCompanyDivisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AffiliatedCompanyDivision>>> GetAffiliatedCompanyDivisions()
        {
            var affiliatedCompanyDivisions = await repository.GetAll();
            if (affiliatedCompanyDivisions == null)
            {
                return NotFound();
            }
            return Ok(affiliatedCompanyDivisions);
        }

        // GET: api/AffiliatedCompanyDivisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AffiliatedCompanyDivision>> GetAffiliatedCompanyDivision(int id)
        {
            var affiliatedCompanyDivision = await repository.GetById(id);
            if (affiliatedCompanyDivision == null)
            {
                return NotFound();
            }
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var fundingResponse = await _httpClient.GetAsync(baseURL + "/AffiliatedCompanyDivisionFundingFiles/ByAffiliatedCompanyDivisionId/" + affiliatedCompanyDivision.Id);
            if (fundingResponse.IsSuccessStatusCode)
            {
                string apiResponseFunding = await fundingResponse.Content.ReadAsStringAsync();
                var affiliatedCompanyDivisionFundingFiles = JsonConvert.DeserializeObject<List<AffiliatedCompanyDivisionFundingFile>>(apiResponseFunding);
                if (affiliatedCompanyDivisionFundingFiles != null)
                {
                    affiliatedCompanyDivision.AffiliatedCompanyDivisionFundingFiles = affiliatedCompanyDivisionFundingFiles;
                }
                else
                {
                    affiliatedCompanyDivision.AffiliatedCompanyDivisionFundingFiles = new List<AffiliatedCompanyDivisionFundingFile>();
                }
            }
            return Ok(affiliatedCompanyDivision);
        }

        [HttpGet("ByAffiliatedCompanyId/{id}")]
        public async Task<ActionResult<IEnumerable<AffiliatedCompanyDivision>>> GetAffiliatedCompanyDivisions(int id)
        {
            var affiliatedCompanyDivisions = await repository.GetAllByCompanyId(entity => entity.AffiliatedCompanyId == id);
            if (affiliatedCompanyDivisions == null)
            {
                return NotFound();
            }
            var baseURL = _configuration.GetValue<string>("BaseURL");

            foreach (var affiliatedCompanyDivision in affiliatedCompanyDivisions)
            {
                var fundingResponse = await _httpClient.GetAsync(baseURL + "/AffiliatedCompanyDivisionFundingFiles/ByAffiliatedCompanyDivisionId/" + affiliatedCompanyDivision.Id);
                if (fundingResponse.IsSuccessStatusCode)
                {
                    string apiResponseFunding = await fundingResponse.Content.ReadAsStringAsync();
                    var affiliatedCompanyDivisionFundingFiles = JsonConvert.DeserializeObject<List<AffiliatedCompanyDivisionFundingFile>>(apiResponseFunding);
                    if (affiliatedCompanyDivisionFundingFiles != null)
                    {
                        affiliatedCompanyDivision.AffiliatedCompanyDivisionFundingFiles = affiliatedCompanyDivisionFundingFiles;
                    }
                    else
                    {
                        affiliatedCompanyDivision.AffiliatedCompanyDivisionFundingFiles = new List<AffiliatedCompanyDivisionFundingFile>();
                    }
                }
            }
            return Ok(affiliatedCompanyDivisions);
        }

        // PUT: api/AffiliatedCompanyDivisions/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAffiliatedCompanyDivision(int id, AffiliatedCompanyDivision affiliatedCompanyDivision)
        {
            if (id != affiliatedCompanyDivision.Id)
            {
                return BadRequest();
            }

            var updatedAffiliatedCompanyDivision = await repository.Update(affiliatedCompanyDivision, id);
            if (updatedAffiliatedCompanyDivision == null)
            {
                return NotFound();
            }
            return Ok(updatedAffiliatedCompanyDivision);
        }

        // POST: api/AffiliatedCompanyDivisions

        [HttpPost]
        public async Task<ActionResult<AffiliatedCompanyDivision>> PostAffiliatedCompanyDivision(AffiliatedCompanyDivision affiliatedCompanyDivision)
        {
            var addedAffiliatedCompanyDivision = await repository.Add(affiliatedCompanyDivision);
            if (addedAffiliatedCompanyDivision == null)
            {
                return NotFound();
            }
            var existingAffiliatedCompany = await affiliatedCompanyRepository.GetById(affiliatedCompanyDivision.AffiliatedCompanyId);
            existingAffiliatedCompany.IsCompanyDivision = "true";
            affiliatedCompanyDivision.IsBrokerorPartnerPayment = "false";
            affiliatedCompanyDivision.IsBrokerorPartnerPaymentForClient = "false";
            affiliatedCompanyDivision.IsDebitConsumerBenefitFunding = "true";
            affiliatedCompanyDivision.IsDebitConsumerBenefitFundingForClient = "true";
            affiliatedCompanyDivision.IsDebitMonthlyAdministrationFee = "true";
            affiliatedCompanyDivision.IsDebitMonthlyAdministrationFeeForClient = "true";
            affiliatedCompanyDivision.IsCreditCobraPremiumRemittance = "true";
            await affiliatedCompanyRepository.Update(existingAffiliatedCompany, affiliatedCompanyDivision.AffiliatedCompanyId);
            return Ok(addedAffiliatedCompanyDivision);
        }

        // DELETE: api/AffiliatedCompanyDivisions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAffiliatedCompanyDivision(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }


        // DELETE: api/ByCompanyId/5
        [HttpDelete("ByCompanyId/{id}")]
        public async Task<IActionResult> DeleteByCompanyId(int id)
        {
            var response = await repository.DeleteByCompanyId(entity => entity.AffiliatedCompanyId == id);
            var existingAffiliatedCompany = await affiliatedCompanyRepository.GetById(id);
            existingAffiliatedCompany.IsCompanyDivision = "false";
            await affiliatedCompanyRepository.Update(existingAffiliatedCompany, id);
            return Ok(response);
        }
    }
}
