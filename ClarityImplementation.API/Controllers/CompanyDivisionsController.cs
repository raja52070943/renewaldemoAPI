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
using System.Net;
using ClarityImplementation.API.Models.Funding;
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyDivisionsController : ControllerBase
    {
        private readonly GenericRepository<CompanyDivision> repository;
        private readonly GenericRepository<Company> companyRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CompanyDivisionsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<CompanyDivision>(context);
            companyRepository = new GenericRepository<Company>(context);

            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/CompanyDivisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDivision>>> GetCompanyDivisions()
        {
            var companyDivisions = await repository.GetAll();
            if (companyDivisions == null)
            {
                return NotFound();
            }
            return Ok(companyDivisions);
        }

        // GET: api/CompanyDivisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDivision>> GetCompanyDivision(int id)
        {
            var companyDivision = await repository.GetById(id);
            if (companyDivision == null)
            {
                return NotFound();
            }
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var fundingResponse = await _httpClient.GetAsync(baseURL + "/CompanyDivisionFundingFiles/ByCompanyDivisionId/" + companyDivision.Id);
            if (fundingResponse.IsSuccessStatusCode)
            {
                string apiResponseFunding = await fundingResponse.Content.ReadAsStringAsync();
                var companyDivisionFundingFiles = JsonConvert.DeserializeObject<List<CompanyDivisionFundingFile>>(apiResponseFunding);
                if (companyDivisionFundingFiles != null)
                {
                    companyDivision.CompanyDivisionFundingFiles = companyDivisionFundingFiles;
                }
                else
                {
                    companyDivision.CompanyDivisionFundingFiles = new List<CompanyDivisionFundingFile>();
                }
            }
            return Ok(companyDivision);
        }

        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<IEnumerable<CompanyDivision>>> GetCompanyDivisions(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var companyDivisions = await repository.GetAllByCompanyId(entity => entity.CompanyId == id);
            if (companyDivisions == null)
            {
                return NotFound();
            }
            foreach (var companyDivision in companyDivisions)
            {
                var fundingResponse = await _httpClient.GetAsync(baseURL + "/CompanyDivisionFundingFiles/ByCompanyDivisionId/" + companyDivision.Id);
                if (fundingResponse.IsSuccessStatusCode)
                {
                    string apiResponseFunding = await fundingResponse.Content.ReadAsStringAsync();
                    var companyDivisionFundingFiles = JsonConvert.DeserializeObject<List<CompanyDivisionFundingFile>>(apiResponseFunding);
                    if (companyDivisionFundingFiles != null)
                    {
                        companyDivision.CompanyDivisionFundingFiles = companyDivisionFundingFiles;
                    }
                    else
                    {
                        companyDivision.CompanyDivisionFundingFiles = new List<CompanyDivisionFundingFile>();
                    }
                }
            }
         
            return Ok(companyDivisions);
        }

        // PUT: api/CompanyDivisions/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyDivision(int id, CompanyDivision companyDivision)
        {
            if (id != companyDivision.Id)
            {
                return BadRequest();
            }

            var updatedCompanyDivision = await repository.Update(companyDivision, id);
            if (updatedCompanyDivision == null)
            {
                return NotFound();
            }
            return Ok(updatedCompanyDivision);
        }

        // POST: api/CompanyDivisions

        [HttpPost]
        public async Task<ActionResult<CompanyDivision>> PostCompanyDivision(CompanyDivision companyDivision)
        {
            var addedCompanyDivision = await repository.Add(companyDivision);
            if (addedCompanyDivision == null)
            {
                return NotFound();
            }
            var existingCompany = await companyRepository.GetById(companyDivision.CompanyId);
            existingCompany.IsCompanyDivision = "true";
            companyDivision.IsBrokerorPartnerPayment = "false";
            companyDivision.IsBrokerorPartnerPaymentForClient = "false";
            companyDivision.IsDebitConsumerBenefitFunding = "true";
            companyDivision.IsDebitConsumerBenefitFundingForClient = "true";
            companyDivision.IsDebitMonthlyAdministrationFee = "true";
            companyDivision.IsDebitMonthlyAdministrationFeeForClient = "true";
            companyDivision.IsCreditCobraPremiumRemittance = "true";
            await companyRepository.Update(existingCompany, companyDivision.CompanyId);
            return Ok(addedCompanyDivision);
        }

        // DELETE: api/CompanyDivisions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyDivision(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

        // DELETE: api/ByCompanyId/5
        [HttpDelete("ByCompanyId/{id}")]
        public async Task<IActionResult> DeleteByCompanyId(int id)
        {
            var response = await repository.DeleteByCompanyId(entity => entity.CompanyId == id);
            var existingCompany = await companyRepository.GetById(id);
            existingCompany.IsCompanyDivision = "false";
            await companyRepository.Update(existingCompany, id);
            return Ok(response);
        }

        //GetAllByCompanyId
        [HttpGet("GetAllDivisionNames/ByCompanyId/{id}")]
        public async Task<ActionResult<IEnumerable<CompanyDivision>>> GetAllDivisionNamesByCompanyId(int id)
        {
            var companyDivisions = await repository.GetAllByCompanyId(entity => entity.CompanyId == id);
            if (companyDivisions == null)
            {
                return NotFound();
            }

            string divisionNames = string.Join(", ", companyDivisions.Select(div => div.DivisionName));
            return Ok(divisionNames);

        }
    }
}
