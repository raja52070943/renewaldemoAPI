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
    public class ContactRolesController : ControllerBase
    {
        private readonly GenericRepository<ContactRole> repository;

        public ContactRolesController(ClarityDbContext context)
        {
            repository = new GenericRepository<ContactRole>(context);
        }

        // GET: api/ContactRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactRole>>> GetContactRoles()
        {
            var contactRoles = await repository.GetAll();
            if (contactRoles == null)
            {
                return NotFound();
            }
            return Ok(contactRoles);
        }

        // GET: api/ContactRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactRole>> GetContactRole(int id)
        {
            var contactRole = await repository.GetById(id);
            if (contactRole == null)
            {
                return NotFound();
            }
            return Ok(contactRole);
        }

        // PUT: api/ContactRoles/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactRole(int id, ContactRole contactRole)
        {
            if (id != contactRole.Id)
            {
                return BadRequest();
            }

            var updatedContactRole = await repository.Update(contactRole, id);
            if (updatedContactRole == null)
            {
                return NotFound();
            }
            return Ok(updatedContactRole);
        }

        // POST: api/ContactRoles

        [HttpPost]
        public async Task<ActionResult<ContactRole>> PostContactRole(ContactRole contactRole)
        {
            var addedContactRole = await repository.Add(contactRole);
            if (addedContactRole == null)
            {
                return NotFound();
            }
            return Ok(addedContactRole);
        }

        // DELETE: api/ContactRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactRole(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }


    }
}
