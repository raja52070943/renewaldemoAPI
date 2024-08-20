using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiNotificationLogsController : Controller
    {
        private readonly GenericRepository<Api_Notification_Log> repository;
        public ApiNotificationLogsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<Api_Notification_Log>(context);

        }

        // GET: api/Logs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Api_Notification_Log>>> GetAllLogs()
        {
            var logs = await repository.GetAll();
            if (logs == null)
            {
                return NotFound();
            }

            return Ok(logs);
        }

        // Get By Id 
        [HttpGet("{notificationId}")]
        public async Task<ActionResult<Api_Notification_Log>> GetLogsById(int notificationId)
        {
            var log = await repository.GetById(notificationId);
            if (log == null)
            {
                return NotFound();
            }
            return Ok(log);
        }

        [HttpGet("case/{caseId}")]
        public async Task<ActionResult<IEnumerable<Api_Notification_Log>>> GetLogsByCaseId(string caseId)
        {
            var logs = await repository.GetEntitiesByCaseId(caseId); // Assuming this method returns a list of logs

            if (logs == null || !logs.Any())
            {
                return NotFound();
            }

            return Ok(logs);
        }



        // PUT: api/Log/5

        [HttpPut("{notificationId}")]
        public async Task<IActionResult> PutFileFileUpload(int notificationId, Api_Notification_Log notificationLog)
        {
            if (notificationId != notificationLog.NotificationId)
            {
                return BadRequest();
            }

            var updatedLog = await repository.Update(notificationLog, notificationId);
            if (updatedLog == null)
            {
                return NotFound();
            }
            return Ok(updatedLog);
        }

        // POST: api/Log
        [HttpPost]
        public async Task<ActionResult<Api_Notification_Log>> PostLog(Api_Notification_Log notificationLog)
        {


            var addedLog = await repository.Add(notificationLog);
            if (addedLog == null)
            {
                return NotFound();
            }
            return Ok(addedLog);
        }
        
    }
}
