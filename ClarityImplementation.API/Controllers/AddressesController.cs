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
    public class AddressesController : ControllerBase
    {
        private readonly GenericRepository<Address> repository;

        public AddressesController(ClarityDbContext context)
        {
            repository = new GenericRepository<Address>(context);
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var contactAddresses = await repository.GetAll();
            if (contactAddresses == null)
            {
                return NotFound();
            }
            return Ok(contactAddresses);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await repository.GetById(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        // GET: api/CompanyAddresses/5
        //[HttpGet("{id}")]
        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<Address>> GetCompanyAddresses(int id)
        {
            var addresses = await repository.GetByCompanyId(entity => entity.CompanyId == id);
            if (addresses == null)
            {
                return NotFound();
            }
            return Ok(addresses);
        }

        // PUT: api/Addresses/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            var updatedAddress = await repository.Update(address, id);
            if (updatedAddress == null)
            {
                return NotFound();
            }
            return Ok(updatedAddress);
        }

        // POST: api/Addresses

        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            address.CreatedOn = DateTime.Now;
            var addedAddress = await repository.Add(address);
            if (addedAddress == null)
            {
                return NotFound();
            }
            return Ok(addedAddress);
        }


        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }


    }
}
