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
    public class BrokerContactsController : ControllerBase
    {
        private readonly GenericRepository<BrokerContact> repository;

        public BrokerContactsController(ClarityDbContext context)
        {
            repository = new GenericRepository<BrokerContact>(context);
        }

        // GET: api/BrokerContacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrokerContact>>> GetBrokerContacts()
        {
            var brokerContacts = await repository.GetAll();
            if (brokerContacts == null)
            {
                return NotFound();
            }
            return Ok(brokerContacts);
        }

        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<IEnumerable<BrokerContact>>> GetBrokerContacts(int id)
        {
            var brokerContacts = await repository.GetAllByCompanyId(entity => entity.CompanyId == id);
            if (brokerContacts == null)
            {
                return NotFound();
            }
            foreach (var brokerContact in brokerContacts)
            {
                if (brokerContact.Role != null && brokerContact.Role!="")
                {
                    string[] selectedRoles = brokerContact.Role.Split(','); // Splitting into an array of strings directly
                    brokerContact.SelectedRoles = selectedRoles.ToList(); // Converting the array to a list of strings
                }

                if (brokerContact.Responsibility != null && brokerContact.Responsibility!="")
                {
                    string[] selectedResponsibilities = brokerContact.Responsibility.Split(','); // Splitting into an array of strings directly
                    brokerContact.SelectedResponsibilities = selectedResponsibilities.ToList(); // Converting the array to a list of strings
                }
            }
            return Ok(brokerContacts);
        }

        // GET: api/BrokerContacts/5
        [HttpGet("{id}")]

        public async Task<ActionResult<BrokerContact>> GetBrokerContact(int id)
        {
            var brokerContact = await repository.GetById(id);
            if (brokerContact == null)
            {
                return NotFound();
            }
            if (brokerContact.Role != null && brokerContact.Role != "")
            {
                string[] selectedRoles = brokerContact.Role.Split(','); // Splitting into an array of strings directly
                brokerContact.SelectedRoles = selectedRoles.ToList(); // Converting the array to a list of strings
            }

            if (brokerContact.Responsibility != null && brokerContact.Responsibility != "")
            {
                string[] selectedResponsibilities = brokerContact.Responsibility.Split(','); // Splitting into an array of strings directly
                brokerContact.SelectedResponsibilities = selectedResponsibilities.ToList(); // Converting the array to a list of strings
            }
            return Ok(brokerContact);
        }

        // PUT: api/BrokerContacts/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrokerContact(int id, BrokerContact brokerContact)
        {
            if (id != brokerContact.Id)
            {
                return BadRequest();
            }

            var updatedBrokerContact = await repository.Update(brokerContact, id);
            if (updatedBrokerContact == null)
            {
                return NotFound();
            }
            return Ok(updatedBrokerContact);
        }

        // POST: api/BrokerContacts

        [HttpPost]
        public async Task<ActionResult<BrokerContact>> PostBrokerContact(BrokerContact brokerContact)
        {
            var addedBrokerContact = await repository.Add(brokerContact);
            if (addedBrokerContact == null)
            {
                return NotFound();
            }
            return Ok(addedBrokerContact);
        }

        // DELETE: api/BrokerContacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrokerContact(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }


    }
}
