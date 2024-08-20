using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRAReimbursableExpensesController : Controller
    {
        
        private readonly GenericRepository<HRAReimbursableExpenses> repository;

        public HRAReimbursableExpensesController(ClarityDbContext context)
        {
            repository = new GenericRepository<HRAReimbursableExpenses>(context);
        }

        // GET: api/HRAReimbursableExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HRAReimbursableExpenses>>> GetHRAReimbursableExpenses()
        {
            var hraReimbursableExpenses = await repository.GetAll();
            if (hraReimbursableExpenses == null)
            {
                return NotFound();
            }
            return Ok(hraReimbursableExpenses);
        }

        // GET: api/HRAReimbursableExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactRole>> GetHRAReimbursableExpense(int id)
        {
            var hraReimbursableExpense = await repository.GetById(id);
            if (hraReimbursableExpense == null)
            {
                return NotFound();
            }
            return Ok(hraReimbursableExpense);
        }

        // PUT: api/HRAReimbursableExpenses/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHRAReimbursableExpense(int id, HRAReimbursableExpenses hraReimbursableExpense)
        {
            if (id != hraReimbursableExpense.Id)
            {
                return BadRequest();
            }

            var updatedHRAReimbursableExpense = await repository.Update(hraReimbursableExpense, id);
            if (updatedHRAReimbursableExpense == null)
            {
                return NotFound();
            }
            return Ok(updatedHRAReimbursableExpense);
        }

        // POST: api/HRAReimbursableExpenses

        [HttpPost]
        public async Task<ActionResult<HRAReimbursableExpenses>> PostHRAReimbursableExpense(HRAReimbursableExpenses hraReimbursableExpense)
        {
            var addedHRAReimbursableExpense = await repository.Add(hraReimbursableExpense);
            if (addedHRAReimbursableExpense == null)
            {
                return NotFound();
            }
            return Ok(addedHRAReimbursableExpense);
        }

        // DELETE: api/HRAReimbursableExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHRAReimbursableExpense(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
