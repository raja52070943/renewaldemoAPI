using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Services.SFCases;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Text;
using System.Xml;

namespace ClarityImplementation.API.Services.Polling
{
    public class PollingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _pollingInterval = TimeSpan.FromMinutes(5);  // Adjust as necessary
        private DateTime _lastCheckedTime;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PollingService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            //_lastCheckedTime = DateTime.UtcNow; // Initialize with the current time or fetch from a stored state
            _lastCheckedTime = DateTime.Now;

            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await PollDataAsync();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Clarity Error occurred: {ex.Message}");
                    // Optionally log to a file or a logging service
                }

                await Task.Delay(_pollingInterval, stoppingToken);
            }
        }

        private async Task PollDataAsync()
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ClarityDbContext>();
                    var newEntities = await dbContext.Api_Cases
                        .Where(entity => entity.CaseStatus =="New")
                        .ToListAsync();

                    //if (newEntities.Any())
                    //{
                    //    _lastCheckedTime = newEntities.Max(e => e.CreatedAt);  // Update the last checked time
                    //}
                    //try
                    //{
                    //    EmailService _emailService = new EmailService();
                    //    var emailModel = new Email
                    //    {
                    //        ToEmail = "
                    //        @claritybenefitsolutions.com",
                    //        Subject = "Test Email - New Implementation",

                    //        Body = "Test Email"
                    //    };
                    //    var result = await _emailService.SendTestEmailAsync(emailModel);
                    //}
                    //catch(Exception exe)
                    //{

                    //}

                    foreach (var entity in newEntities)
                    {
                        await ProcessEntity(entity, dbContext);
                    }
                    // ProcessEntity(newEntities, dbContext);

                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Failed to process data: {ex.Message}");
                    throw;
                }
            }
        }

        private async Task ProcessEntity(Api_Case entity, ClarityDbContext dbContext)
        {
            // Add your processing logic here
            //Console.WriteLine($"Processing new entity created at: {entity.CreatedAt} : {entity.CaseId}");
            if (entity.CaseStatus == "New")
            {
                var contactEntities = await dbContext.Api_Case_Contacts
                        .Where(contact => contact.CaseId == entity.CaseId)
                        .ToListAsync();

     //           var company = await dbContext.Companies
     //.Where(item => item.case_id == entity.CaseId)
     //.FirstOrDefaultAsync();

                EmailService _emailService = new EmailService();
               
                
               
                //Check plan and Implementation Contact
                if (entity.ImplementationPlanType != null && entity.Case_Implementation_Contact_Email != null && entity.Case_Implementation_Contact_First_Name != null)
                {
                    try
                    {
                        var emailModel = new Email
                        {
                            ToEmail = "donotreply@claritybenefitsolutions.com",
                            Subject = "Please Register for the Clarity Benefit Solutions Portal and Begin Your Implementation - New Implementation",
                            CaseID = entity.CaseId,
                            RecipientName = entity.LegalBusinessName,
                            CompanyName = entity.LegalBusinessName,
                            FirstName = entity.Case_Implementation_Contact_First_Name,
                            LastName = entity.Case_Implementation_Contact_Last_Name,
                            CaseOwnerFirstName = entity.Case_Owner_Name,
                            CaseOwnerLastName = "",
                            CaseOwnerEmail = entity.Case_Owner_Email,
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
                          { "{{company}}", emailModel.CompanyName },
                          { "{{is_name}}", emailModel.CaseOwnerFirstName+" "+emailModel.CaseOwnerLastName },
                          { "{{is_position_title}}", "Client Engagement manager" },
                          { "{{is_email}}", emailModel.CaseOwnerEmail },
                          { "{{first_name}}", emailModel.FirstName },
                          { "{{last_name}}", emailModel.LastName },
                          { "{{caseOwner_first_name}}", emailModel.CaseOwnerFirstName },
                          { "{{caseOwner_last_name}}", emailModel.CaseOwnerLastName },
                          { "{{curr_user_first_name}}", emailModel.FirstName },
                          { "{{curr_user_last_name}}", emailModel.LastName },
                          { "{{implementation_contact_email}}", entity.Case_Implementation_Contact_Email }
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

                        var emailLog = new Api_Notification_Log
                        {
                            DestinationAddress = emailModel.ToEmail,
                            CaseId = emailModel.CaseID,
                            NotificationContent = emailModel.Body,
                            CreatedAt = DateTime.Now,
                            CreatedBy = emailModel.ToEmail,
                            NotificationSentAt = DateTime.Now,
                            NotificationLabel = emailModel.Subject,


                        };

                        


                        try
                        {
                            await _emailService.SendEmailAsync(emailModel);

                        }
                        catch (Exception ex)
                        {
                            

                        }
                        finally
                        {
                            dbContext.Api_Notification_Logs.Add(emailLog);
                            await dbContext.SaveChangesAsync();

                        }

                    }
                    catch(Exception ex)
                    {

                    }
                    //Send Welcome Email to Implementation Contact
                    //Email email = new Email
                    //{
                    //    toemail = "donotreply@claritybenefitsolutions.com",
                    //    subject = "please register for the clarity benefit solutions portal and begin your implementation - new implementation",
                    //    caseid = entity.caseid,
                    //    recipientname = entity.legalbusinessname,
                    //    firstname = entity.case_implementation_contact_first_name,
                    //    lastname = entity.case_implementation_contact_last_name,
                    //    caseownerfirstname = entity.case_owner_name,
                    //    caseownerlastname = "",
                    //    caseowneremail = entity.case_owner_email,
                    //    template = "emailcopy1.html",
                    //    noofdays = 0,
                    //    noofreminders = 2
                    //};
                    //var json = jsonconvert.serializeobject(emailmodel);
                    //var content = new stringcontent(json, encoding.utf8, "application/json");

                    //var baseurl = _configuration.getvalue<string>("baseurl");
                    //var sendemail = _httpclient.postasync(baseurl + "/email/send", content);
                    //add case
                    //chnage status clientnotified
                    // casesservice casesservice = new casesservice(entity, dbcontext);
                    //await casesservice.addcase(entity);
                    CasesService casesService = new CasesService(entity, dbContext);
                    await casesService.AddCase(entity);
                }
                else
                {

                    //Send Case Failure Email
                    //var emailModel = new Email
                    //{
                    //    ToEmail = "donotreply@claritybenefitsolutions.com",
                    //    Subject = $"Implementation - Case {entity.LegalBusinessName} - {entity.SfCaseId} Case Rollout failed",
                    //    CaseID = entity.CaseId,
                    //    CompanyName = entity.LegalBusinessName,
                    //    FirstName = entity.Case_Owner_Name,
                    //    LastName = "",
                    //    CaseOwnerFirstName = entity.Case_Owner_Name,
                    //    CaseOwnerLastName = "",
                    //    CaseOwnerEmail = entity.Case_Owner_Email,
                    //    Template = "FailureEmail.html",

                    //};
                    //var json = JsonConvert.SerializeObject(emailModel);
                    //var content = new StringContent(json, Encoding.UTF8, "application/json");

                    //var baseURL = _configuration.GetValue<string>("BaseURL");
                    //var sendEmail = _httpClient.PostAsync(baseURL + "/Email/send", content);
                    try
                    {
                        var emailModel = new Email
                        {
                            ToEmail = "donotreply@claritybenefitsolutions.com",
                            Subject = $"Implementation - Case {entity.LegalBusinessName} - {entity.SfCaseId} Case Rollout failed",
                            CaseID = entity.CaseId,
                            RecipientName = entity.LegalBusinessName,
                            CompanyName = entity.LegalBusinessName,
                            FirstName = entity.Case_Implementation_Contact_First_Name,
                            LastName = entity.Case_Implementation_Contact_Last_Name,
                            CaseOwnerFirstName = entity.Case_Owner_Name,
                            CaseOwnerLastName = "",
                            CaseOwnerEmail = entity.Case_Owner_Email,
                            Template = "FailureEmail.html"
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
                        await _emailService.SendEmailAsync(emailModel);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
        }
    }
}
