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
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffiliatedCompaniesController : ControllerBase
    {
        private readonly GenericRepository<AffiliatedCompany> repository;
        private readonly GenericRepository<Company> companyRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AffiliatedCompaniesController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<AffiliatedCompany>(context);
            companyRepository = new GenericRepository<Company>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/AffiliatedCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AffiliatedCompany>>> GetAffiliatedCompanies()
        {
            var affiliatedCompanies = await repository.GetAll();
            if (affiliatedCompanies == null)
            {
                return NotFound();
            }
            return Ok(affiliatedCompanies);
        }

        // GET: api/AffiliatedCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AffiliatedCompany>> GetAffiliatedCompany(int id)
        {
            var affiliatedCompany = await repository.GetById(id);
            if (affiliatedCompany == null)
            {
                return NotFound();
            }
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var fundingResponse = await _httpClient.GetAsync(baseURL + "/AffiliatedCompanyFundingFiles/ByAffiliatedCompanyId/" + affiliatedCompany.Id);
            if (fundingResponse.IsSuccessStatusCode)
            {
                string apiResponseFunding = await fundingResponse.Content.ReadAsStringAsync();
                var affiliatedCompanyFundingFiles = JsonConvert.DeserializeObject<List<AffiliatedCompanyFundingFile>>(apiResponseFunding);
                if (affiliatedCompanyFundingFiles != null)
                {
                    affiliatedCompany.AffiliatedCompanyFundingFiles = affiliatedCompanyFundingFiles;
                }
                else
                {
                    affiliatedCompany.AffiliatedCompanyFundingFiles = new List<AffiliatedCompanyFundingFile>();
                }
            }
            return Ok(affiliatedCompany);
        }

        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<IEnumerable<AffiliatedCompany>>> GetAffiliatedCompanies(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var affiliatedCompanies = await repository.GetAllByCompanyId(entity => entity.CompanyId == id);
            if (affiliatedCompanies == null)
            {
                return NotFound();
            }
            foreach (AffiliatedCompany affiliated in affiliatedCompanies)
            {
                var response = await _httpClient.GetAsync(baseURL + "/AffiliatedCompanyDivisions/ByAffiliatedCompanyId/" + affiliated.Id);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var affiliatedCompanyDivisions = JsonConvert.DeserializeObject<List<AffiliatedCompanyDivision>>(apiResponse);
                    affiliated.AffiliatedCompanyDivisions = affiliatedCompanyDivisions;
                }
            }
            foreach (var affiliatedCompany in affiliatedCompanies)
            {
                var fundingResponse = await _httpClient.GetAsync(baseURL + "/AffiliatedCompanyFundingFiles/ByAffiliatedCompanyId/" + affiliatedCompany.Id);
                if (fundingResponse.IsSuccessStatusCode)
                {
                    string apiResponseFunding = await fundingResponse.Content.ReadAsStringAsync();
                    var affiliatedCompanyFundingFiles = JsonConvert.DeserializeObject<List<AffiliatedCompanyFundingFile>>(apiResponseFunding);
                    if (affiliatedCompanyFundingFiles != null)
                    {
                        affiliatedCompany.AffiliatedCompanyFundingFiles = affiliatedCompanyFundingFiles;
                    }
                    else
                    {
                        affiliatedCompany.AffiliatedCompanyFundingFiles = new List<AffiliatedCompanyFundingFile>();
                    }
                }
            }
         
            return Ok(affiliatedCompanies);
        }

        // PUT: api/AffiliatedCompanies/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAffiliatedCompany(int id, AffiliatedCompany affiliatedCompany)
        {
            if (id != affiliatedCompany.Id)
            {
                return BadRequest();
            }

            var updatedAffiliatedCompany = await repository.Update(affiliatedCompany, id);
            if (updatedAffiliatedCompany == null)
            {
                return NotFound();
            }
            return Ok(updatedAffiliatedCompany);
        }

        // POST: api/AffiliatedCompanies

        [HttpPost]
        public async Task<ActionResult<AffiliatedCompany>> PostAffiliatedCompany(AffiliatedCompany affiliatedCompany)
        {
            var addedAffiliatedCompany = await repository.Add(affiliatedCompany);
            if (addedAffiliatedCompany == null)
            {
                return NotFound();
            }
            var existingCompany = await companyRepository.GetById(affiliatedCompany.CompanyId);
            existingCompany.IsAffiliatedCompany = "true";
            affiliatedCompany.IsBrokerorPartnerPayment = "false";
            affiliatedCompany.IsBrokerorPartnerPaymentForClient = "false";
            affiliatedCompany.IsDebitConsumerBenefitFunding = "true";
            affiliatedCompany.IsDebitConsumerBenefitFundingForClient = "true";
            affiliatedCompany.IsDebitMonthlyAdministrationFee = "true";
            affiliatedCompany.IsDebitMonthlyAdministrationFeeForClient = "true";
            affiliatedCompany.IsCreditCobraPremiumRemittance = "true";
            await companyRepository.Update(existingCompany, affiliatedCompany.CompanyId);
            return Ok(addedAffiliatedCompany);
        }

        // DELETE: api/AffiliatedCompanies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAffiliatedCompany(int id)
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
            existingCompany.IsAffiliatedCompany = "false";
            await companyRepository.Update(existingCompany, id);
            return Ok(response);
        }

        [HttpGet("GetAllDivisionNames/ByCompanyId/{id}")]
        public async Task<ActionResult<string>> GetAllAffiliatedCompanyDivisionNames(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var affiliatedCompanies = await repository.GetAllByCompanyId(entity => entity.CompanyId == id);
            if (affiliatedCompanies == null || !affiliatedCompanies.Any())
            {
                return NotFound("No affiliated companies found.");
            }

            // Create a list to store all division names
            List<string> allDivisionNames = new List<string>();

            foreach (AffiliatedCompany affiliated in affiliatedCompanies)
            {
                var response = await _httpClient.GetAsync(baseURL + "/AffiliatedCompanyDivisions/ByAffiliatedCompanyId/" + affiliated.Id);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var affiliatedCompanyDivisions = JsonConvert.DeserializeObject<List<AffiliatedCompanyDivision>>(apiResponse);

                    // Add division names to the list
                    allDivisionNames.AddRange(affiliatedCompanyDivisions.Select(div => div.DivisionName));
                }
            }

            // Join all division names into a comma-separated string
            string allDivisionNamesString = string.Join(", ", allDivisionNames);

            return Ok(allDivisionNamesString);
        }

        [HttpGet("GetAllAffiliateNames/ByCompanyId/{id}")]
        public async Task<ActionResult<string>> GetAllAffiliateNames(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var affiliatedCompanies = await repository.GetAllByCompanyId(entity => entity.CompanyId == id);
            if (affiliatedCompanies == null || !affiliatedCompanies.Any())
            {
                return NotFound("No affiliated companies found.");
            }

            // Create a list to store all affiliate  names
            List<string> allAffiliateNames = new List<string>();

            foreach (AffiliatedCompany affiliated in affiliatedCompanies)
            {
                allAffiliateNames.Add(affiliated.AffiliateName);

            }

            // Join all division names into a comma-separated string
            string allAffiliateNamesString = string.Join(", ", allAffiliateNames);

            return Ok(allAffiliateNamesString);
        }



    }
}
