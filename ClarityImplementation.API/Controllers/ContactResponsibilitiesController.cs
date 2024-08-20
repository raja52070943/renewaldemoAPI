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
    public class ContactResponsibilitiesController : ControllerBase
    {
        private readonly GenericRepository<ContactResponsibility> repository;

        public ContactResponsibilitiesController(ClarityDbContext context)
        {
            repository = new GenericRepository<ContactResponsibility>(context);
        }

        // GET: api/ContactResponsibilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactResponsibility>>> GetContactResponsibilities()
        {
            var contactResponsibilities = await repository.GetAll();
            if (contactResponsibilities == null)
            {
                return NotFound();
            }
            return Ok(contactResponsibilities);
        }

        // GET: api/ContactResponsibilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactResponsibility>> GetContactResponsibility(int id)
        {
            var contactResponsibility = await repository.GetById(id);
            if (contactResponsibility == null)
            {
                return NotFound();
            }
            return Ok(contactResponsibility);
        }

        // PUT: api/ContactResponsibilities/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactResponsibility(int id, ContactResponsibility contactResponsibility)
        {
            if (id != contactResponsibility.Id)
            {
                return BadRequest();
            }

            var updatedContactResponsibility = await repository.Update(contactResponsibility, id);
            if (updatedContactResponsibility == null)
            {
                return NotFound();
            }
            return Ok(updatedContactResponsibility);
        }

        // POST: api/ContactResponsibilities

        [HttpPost]
        public async Task<ActionResult<ContactResponsibility>> PostContactResponsibility(ContactResponsibility contactResponsibility)
        {
            var addedContactResponsibility = await repository.Add(contactResponsibility);
            if (addedContactResponsibility == null)
            {
                return NotFound();
            }
            return Ok(addedContactResponsibility);
        }

        // DELETE: api/ContactResponsibilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactResponsibility(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }


    }
}
