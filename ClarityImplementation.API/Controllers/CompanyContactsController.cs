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

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyContactsController : ControllerBase
    {
        private readonly GenericRepository<CompanyContact> repository;

        public CompanyContactsController(ClarityDbContext context)
        {
            repository = new GenericRepository<CompanyContact>(context);
        }

        // GET: api/CompanyContacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyContact>>> GetCompanyContacts()
        {
            var companyContacts = await repository.GetAll();
            if (companyContacts == null)
            {
                return NotFound();
            }
            foreach (var companyContact in companyContacts)
            {
                companyContact.Phone = FormatPhoneNumber(companyContact.Phone);
            }
            return Ok(companyContacts);
        }

        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<IEnumerable<CompanyContact>>> GetCompanyContacts(int id)
        {
            var companyContacts = await repository.GetAllByCompanyId(entity => entity.CompanyId == id);
            if (companyContacts == null)
            {
                return NotFound();
            }

            foreach (var companyContact in companyContacts)
            {
                //if (companyContact.Role != null && companyContact.Role!="")
                //{
                //    string[] selectedRoles = companyContact.Role.Split(','); 
                //    companyContact.SelectedRoles = selectedRoles.ToList(); 
                //}
                //if (companyContact.Responsibility != null && companyContact.Responsibility!="")
                //{
                //    string[] selectedResponsibilities = companyContact.Responsibility.Split(','); 
                //    companyContact.SelectedResponsibilities = selectedResponsibilities.ToList();
                //}

                companyContact.Phone = FormatPhoneNumber(companyContact.Phone);
            }

            return Ok(companyContacts);
        }


        // Helper method to format phone number
        private string FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return string.Empty;
            }

            
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            
            if (phoneNumber.Length < 10)
            {
                return phoneNumber;
            }

            if (phoneNumber.Length > 10)
            {
                phoneNumber = phoneNumber.Substring(phoneNumber.Length - 10);
            }

            return string.Format("({0}) {1} {2}",
                phoneNumber.Substring(0, 3),
                phoneNumber.Substring(3, 3),
                phoneNumber.Substring(6));
        }



        // GET: api/CompanyContacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyContact>> GetCompanyContact(int id)
        {
            var companyContact = await repository.GetById(id);
            if (companyContact == null)
            {
                return NotFound();
            }

            if (companyContact.Role != null && companyContact.Role != "")
            {
                string[] selectedRoles = companyContact.Role.Split(','); // Splitting into an array of strings directly
                companyContact.SelectedRoles = selectedRoles.ToList(); // Converting the array to a list of strings
            }
            if (companyContact.Responsibility != null && companyContact.Responsibility != "")
            {
                string[] selectedResponsibilities = companyContact.Responsibility.Split(','); // Splitting into an array of strings directly
                companyContact.SelectedResponsibilities = selectedResponsibilities.ToList(); // Converting the array to a list of strings
            }

            return Ok(companyContact);
        }


        // PUT: api/CompanyContacts/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyContact(int id, CompanyContact companyContact)
        {
            if (id != companyContact.Id)
            {
                return BadRequest();
            }

            var updatedCompanyContact = await repository.Update(companyContact, id);
            if (updatedCompanyContact == null)
            {
                return NotFound();
            }
            return Ok(updatedCompanyContact);
        }

        // POST: api/CompanyContacts

        [HttpPost]
        public async Task<ActionResult<CompanyContact>> PostCompanyContact(CompanyContact companyContact)
        {
            var addedCompanyContact = await repository.Add(companyContact);
            if (addedCompanyContact == null)
            {
                return NotFound();
            }
            return Ok(addedCompanyContact);
        }

        // DELETE: api/CompanyContacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyContact(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }


    }
}
