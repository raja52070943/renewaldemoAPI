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
using System.Configuration;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly GenericRepository<Page> repository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PagesController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<Page>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/Pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetPages()
        {
            var pages = await repository.GetAll();
            if (pages == null)
            {
                return NotFound();
            }
            return Ok(pages);
        }

        // GET: api/Pages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Page>> GetPage(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var page = await repository.GetById(id);
            if (page == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync(baseURL + "/PageMetaDataFields/ByPageId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var pageMetaDataFields = JsonConvert.DeserializeObject<List<PageMetaDataField>>(apiResponse);
                page.PageMetaDataFields = pageMetaDataFields;
            }
            return Ok(page);
        }

        // PUT: api/Pages/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPage(int id, Page page)
        {
            if (id != page.Id)
            {
                return BadRequest();
            }

            var updatedPage = await repository.Update(page, id);
            if (updatedPage == null)
            {
                return NotFound();
            }
            return Ok(updatedPage);
        }

        // POST: api/Pages

        [HttpPost]
        public async Task<ActionResult<Page>> PostPage(Page page)
        {
            var addedPage = await repository.Add(page);
            if (addedPage == null)
            {
                return NotFound();
            }
            return Ok(addedPage);
        }

        // DELETE: api/Pages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }


    }
}