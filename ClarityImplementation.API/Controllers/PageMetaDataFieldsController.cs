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

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageMetaDataFieldsController : ControllerBase
    {
        private readonly GenericRepository<PageMetaDataField> repository;

        public PageMetaDataFieldsController(ClarityDbContext context)
        {
            repository = new GenericRepository<PageMetaDataField>(context);
        }

        // GET: api/PageMetaDataFields
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PageMetaDataField>>> GetPageMetaDataFields()
        {
            var pageMetaDataFields = await repository.GetAll();
            if (pageMetaDataFields == null)
            {
                return NotFound();
            }
            return Ok(pageMetaDataFields);
        }

        // GET: api/PageMetaDataFields/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PageMetaDataField>> GetPageMetaDataField(int id)
        {
            var pageMetaDataField = await repository.GetById(id);
            if (pageMetaDataField == null)
            {
                return NotFound();
            }
            return Ok(pageMetaDataField);
        }

        // GET: api/PageMetaDataFields/5
        //[HttpGet("{id}")]
        [HttpGet("ByPageId/{id}")]
        public async Task<ActionResult<Address>> GetPageMetaDataFields(int id)
        {
            var pageMetaDataFields = await repository.GetAllByCompanyId(entity => entity.PageId == id);
            if (pageMetaDataFields == null)
            {
                return NotFound();
            }
            return Ok(pageMetaDataFields);
        }

        // PUT: api/PageMetaDataFields/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPageMetaDataField(int id, PageMetaDataField pageMetaDataField)
        {
            if (id != pageMetaDataField.Id)
            {
                return BadRequest();
            }

            var updatedPageMetaDataField = await repository.Update(pageMetaDataField, id);
            if (updatedPageMetaDataField == null)
            {
                return NotFound();
            }
            return Ok(updatedPageMetaDataField);
        }

        // POST: api/PageMetaDataFields

        [HttpPost]
        public async Task<ActionResult<PageMetaDataField>> PostPageMetaDataField(PageMetaDataField pageMetaDataField)
        {
            var addedPageMetaDataField = await repository.Add(pageMetaDataField);
            if (addedPageMetaDataField == null)
            {
                return NotFound();
            }
            return Ok(addedPageMetaDataField);
        }

        // DELETE: api/PageMetaDataFields/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePageMetaDataField(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }


    }
}
