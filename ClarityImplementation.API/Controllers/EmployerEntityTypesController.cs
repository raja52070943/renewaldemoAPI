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
    public class EmployerEntityTypesController : ControllerBase
    {
        private readonly GenericRepository<EmployerEntityType> repository;

        public EmployerEntityTypesController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployerEntityType>(context);
        }

        // GET: api/EmployerEntityTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployerEntityType>>> GetEmployerEntityTypes()
        {
            var employerEntityTypes = await repository.GetAll();
            if (employerEntityTypes == null)
            {
                return NotFound();
            }
            return Ok(employerEntityTypes);
        }

        // GET: api/EmployerEntityTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployerEntityType>> GetEmployerEntityType(int id)
        {
            var employerEntityType = await repository.GetById(id);
            if (employerEntityType == null)
            {
                return NotFound();
            }
            return Ok(employerEntityType);
        }

        // PUT: api/EmployerEntityTypes/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployerEntityType(int id, EmployerEntityType employerEntityType)
        {
            if (id != employerEntityType.Id)
            {
                return BadRequest();
            }

            var updatedEmployerEntityType = await repository.Update(employerEntityType, id);
            if (updatedEmployerEntityType == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployerEntityType);
        }
        // POST: api/EmployerEntityTypes
       
        [HttpPost]
        public async Task<ActionResult<EmployerEntityType>> PostEmployerEntityType(EmployerEntityType employerEntityType)
        {
            var addedEmployerEntityType = await repository.Add(employerEntityType);
            if (addedEmployerEntityType == null)
            {
                return NotFound();
            }
            return Ok(addedEmployerEntityType);
        }

        // DELETE: api/EmployerEntityTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployerEntityType(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

    }
}
