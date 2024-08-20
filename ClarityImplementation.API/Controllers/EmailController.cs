using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using ClarityImplementation.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using NuGet.Protocol.Core.Types;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly GenericRepository<Company> repository;
        private readonly GenericRepository<Email> emailRepository;
        private readonly EmailService _emailService;
        


        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmailController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<Company>(context);
            emailRepository = new GenericRepository<Email>(context);
            
            _emailService = new EmailService();
           


            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpGet("emailtest")]
        public async Task<IActionResult> TestEmail()
        {
            var emailModel = new Email
            {
               

                ToEmail = "noreply@claritybenefitsolutions.com",
                Subject = "Please Register for the Clarity Benefit Solutions Portal and Begin Your Implementation - New Implementation",
                RecipientName = "340B Health",
                Isemail = "dunderwood@claritybenefitsolutions.com",
                Isname = "duderwood",
                FirstName = "Patrick ",
                LastName = "McGary",
                PositionTitle = "Client Engagement Manager",
                Template = "EmailCopy1.html"
            };

            var htmlFilePath = "Templates/" + emailModel.Template;

            if (System.IO.File.Exists(htmlFilePath))
            {
                emailModel.Body = System.IO.File.ReadAllText(htmlFilePath);
                emailModel.EmailSentdate = DateTime.Now;
                var replacements = new Dictionary<string, string>
                    {
                          { "{{RecipientName}}", emailModel.RecipientName },
                          { "{{company}}", emailModel.RecipientName },
                          { "{{is_name}}", emailModel.CaseOwnerFirstName+" "+emailModel.CaseOwnerLastName },
                          { "{{is_position_title}}", "Client Engagement manager" },
                          { "{{is_email}}", emailModel.CaseOwnerEmail },
                          { "{{first_name}}", emailModel.FirstName },
                          { "{{last_name}}", emailModel.LastName },
                          { "{{caseOwner_first_name}}", emailModel.CaseOwnerFirstName },
                          { "{{caseOwner_last_name}}", emailModel.CaseOwnerLastName },
                          { "{{curr_user_first_name}}", emailModel.FirstName },
                          { "{{curr_user_last_name}}", emailModel.LastName }
                    };

                foreach (var replacement in replacements)
                {
                    emailModel.Body = emailModel.Body.Replace(replacement.Key, replacement.Value);
                }

                //var addedEmail = await emailRepository.Add(email);
                //if (addedEmail == null)
                //{
                //    return NotFound();
                //}
            }
            var result = await _emailService.SendTestEmailAsync(emailModel);
            return Ok(result);
        }

        [HttpPost("send")]
        public IActionResult SendEmailToCompany(Email email)
        {
            var result = SendEmailModel(email);
            // Schedule the initial check after 3 days
            var initialCheckDelay = TimeSpan.FromMinutes(1);
           

            return Ok(result);
        }

        

        [HttpPost("SendInvite")]
        public async Task<IActionResult> SendEmailToContactAsync(Email email)
        {

            var htmlFilePath = "Templates/" + email.Template;

            if (System.IO.File.Exists(htmlFilePath))
            {
                email.Body = System.IO.File.ReadAllText(htmlFilePath);
                email.EmailSentdate = DateTime.Now;
                var replacements = new Dictionary<string, string>
                    {
                          { "{{RecipientName}}", email.RecipientName },
                          { "{{company}}", email.CompanyName },
                          { "{{is_name}}", email.CaseOwnerFirstName+" "+email.CaseOwnerLastName },
                          { "{{is_position_title}}", "Client Engagement manager" },
                          { "{{is_email}}", email.CaseOwnerEmail },
                          { "{{first_name}}", email.FirstName },
                          { "{{last_name}}", email.LastName },
                          { "{{caseOwner_first_name}}", email.CaseOwnerFirstName },
                          { "{{caseOwner_last_name}}", email.CaseOwnerLastName },
                          { "{{curr_user_first_name}}", email.FirstName },
                          { "{{curr_user_last_name}}", email.LastName }
                    };

                foreach (var replacement in replacements)
                {
                    email.Body = email.Body.Replace(replacement.Key, replacement.Value);
                }


            }
            else
            {
               
                return Ok("Error: Template file not found.");
            }

            // Your email sending logic goes here

            await _emailService.SendEmailAsync(email);



            return Ok("Success: Email sent after delay.");
        }


        [HttpPost("SendChangeContactRequest")]
        public async Task<IActionResult> SendEmailToChangeContactRequest(Email email)
        {

            var htmlFilePath = "Templates/" + email.Template;

            if (System.IO.File.Exists(htmlFilePath))
            {
                email.Body = System.IO.File.ReadAllText(htmlFilePath);
                email.EmailSentdate = DateTime.Now;
                var replacements = new Dictionary<string, string>
                    {
                          { "{{RecipientName}}", email.RecipientName },
                          { "{{company}}", email.CompanyName },
                          { "{{is_name}}", email.CaseOwnerFirstName+" "+email.CaseOwnerLastName },
                          { "{{is_position_title}}", "Client Engagement manager" },
                          { "{{is_email}}", email.CaseOwnerEmail },
                          { "{{first_name}}", email.FirstName },
                          { "{{last_name}}", email.LastName },
                          { "{{caseOwner_first_name}}", email.CaseOwnerFirstName },
                          { "{{caseOwner_last_name}}", email.CaseOwnerLastName },
                          { "{{curr_user_first_name}}", email.FirstName },
                          { "{{curr_user_last_name}}", email.LastName }
                    };

                foreach (var replacement in replacements)
                {
                    email.Body = email.Body.Replace(replacement.Key, replacement.Value);
                }


            }
            else
            {

                return Ok("Error: Template file not found.");
            }

            // Your email sending logic goes here

            await _emailService.SendEmailAsync(email);



            return Ok("Success: Email sent after delay.");
        }



        [NonAction]
        public async Task<IActionResult> SendEmailModel(Email email)
        {
            

            var htmlFilePath = "Templates/" + email.Template;

            if (System.IO.File.Exists(htmlFilePath))
            {
                email.Body = System.IO.File.ReadAllText(htmlFilePath);
                email.EmailSentdate = DateTime.Now;
                var replacements = new Dictionary<string, string>
                    {
                          { "{{RecipientName}}", email.RecipientName },
                          { "{{company}}", email.RecipientName },
                          { "{{is_name}}", email.CaseOwnerFirstName+" "+email.CaseOwnerLastName },
                          { "{{is_position_title}}", "Client Engagement manager" },
                          { "{{is_email}}", email.CaseOwnerEmail },
                          { "{{first_name}}", email.FirstName },
                          { "{{last_name}}", email.LastName },
                          { "{{caseOwner_first_name}}", email.CaseOwnerFirstName },
                          { "{{caseOwner_last_name}}", email.CaseOwnerLastName },
                          { "{{curr_user_first_name}}", email.FirstName },
                          { "{{curr_user_last_name}}", email.LastName }
                    };

                foreach (var replacement in replacements)
                {
                    email.Body = email.Body.Replace(replacement.Key, replacement.Value);
                }

                //var addedEmail = await emailRepository.Add(email);
                //if (addedEmail == null)
                //{
                //    return NotFound();
                //}
            }
            else
            {
                return Ok("Error: Template file not found.");
            }

            // Your email sending logic goes here
            await _emailService.SendEmailAsync(email);




            return Ok("Success: Email sent after delay.");
        }

        [HttpPost("SendFileUploadReminder")]
        public IActionResult SendFileUploadEmail(Email email)
        {
            //var result = SendEmailAsync(email);
            //var initialCheckDelay = TimeSpan.FromMinutes(0);
            //_backgroundJobClient.Schedule(() => CheckFirstReminderConditions(email), initialCheckDelay);

           

            return Ok();
        }

        [NonAction]
        public async Task CheckFirstReminderConditions(Email email)
        {
            var company = await repository.GetById(email.CompanyId);
            if (company != null && company.CompanyStatus == "Client Notified")
            {
                // Send the initial email

                await SendFollowUpEmail(email, "Reminder1: It's Time to Begin Your Clarity Plan(s) Implementation - New Implementation", "EmailCopy2.html");
                // Schedule the first reminder after 3 days
                var firstReminderDelay = TimeSpan.FromMinutes(1);
               
            }
           
            if (company != null && company.CompanyStatus == "Upload Files")
            {
                SendEmailModel(email);
            }
        }
        [NonAction]
        public async Task CheckSecondReminderConditions(Email email)
        {
            var company = await repository.GetById(email.CompanyId);
            if (company != null && company.CompanyStatus == "Client Notified")
            {


                await SendFollowUpEmail(email, "Reminder2: It's Time to Begin Your Clarity Plan(s) Implementation - New Implementation", "EmailCopy3.html");


            }
        }

        [NonAction]
        public async Task SendFollowUpEmail(Email email, string subject, string template)
        {
            var followUpEmail = new Email
            {
                ToEmail = email.ToEmail,
                Subject = subject,
                RecipientName = email.RecipientName,
                Isemail = email.Isemail,
                Isname = email.Isname,
                FirstName = email.FirstName,
                LastName = email.LastName,
                PositionTitle = email.PositionTitle,
                Template = template
            };

           
        }


    }
}

