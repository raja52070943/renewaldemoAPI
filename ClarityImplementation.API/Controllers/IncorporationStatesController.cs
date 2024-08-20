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
    public class IncorporationStatesController : ControllerBase
    {
        private readonly GenericRepository<IncorporationState> repository;

        public IncorporationStatesController(ClarityDbContext context)
        {
            repository = new GenericRepository<IncorporationState>(context);
        }

        // GET: api/IncorporationStates
        [HttpGet]
        public async Task<IActionResult> GetIncorporationStates()
        {
            var incorporationStates = await repository.GetAll();
            if (incorporationStates == null)
            {
                return NotFound();
            }
            return Ok(incorporationStates);
        }

        // GET: api/IncorporationStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncorporationState>> GetIncorporationState(int id)
        {
            var incorporationState = await repository.GetById(id);
            if (incorporationState == null)
            {
                return NotFound();
            }
            return Ok(incorporationState);
        }

        // PUT: api/IncorporationStates/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncorporationState(int id, IncorporationState incorporationState)
        {
            if (id != incorporationState.Id)
            {
                return BadRequest();
            }

            var updatedIncorporationState = await repository.Update(incorporationState, id);
            if (updatedIncorporationState == null)
            {
                return NotFound();
            }
            return Ok(updatedIncorporationState);
        }

        // POST: api/IncorporationStates

        [HttpPost]
        public async Task<ActionResult<IncorporationState>> PostIncorporationState(IncorporationState incorporationState)
        {
            var addedIncorporationState = await repository.Add(incorporationState);
            if (addedIncorporationState == null)
            {
                return NotFound();
            }
            return Ok(addedIncorporationState);
        }

        // DELETE: api/IncorporationStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncorporationState(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

    }
}
