using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Operations;

namespace ClarityImplementation.API.Services
{
    public class TimedBackgroundService : BackgroundService
    {
        private readonly ILogger<TimedBackgroundService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);
        private readonly IServiceProvider _serviceProvider;

        public TimedBackgroundService(ILogger<TimedBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Notification Service running at: {time}", DateTimeOffset.Now);
                await CheckAndSendNotifications(stoppingToken);
                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task CheckAndSendNotifications(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClarityDbContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
                var now = DateTime.UtcNow;

                var companiesForFirstReminder = dbContext.Companies
                    .Where(c => c.CompanyStatus == "Client Notified" && c.CreatedOn <= now.AddDays(-3) && c.Reminder1 != "Sent")
                    .ToList();

                var companiesForSecondReminder = dbContext.Companies
                    .Where(c => c.CompanyStatus == "Client Notified" && c.Reminder1 == "Sent" && c.Reminder1SentDate <= now.AddDays(-3) && c.Reminder2 != "Sent")
                    .ToList();
                
                var companiesForThirdReminder = dbContext.Companies
                   .Where(c => c.CompanyStatus != "Completed" && c.Reminder2 == "Sent" && c.Reminder2SentDate <= now.AddDays(-3) && c.Reminder3 != "Sent")
                   .ToList();

                var fileUploadEmail = (from c in dbContext.Companies
                                       where c.CompanyStatus == "File Upload" && c.UploadEnrollEmail != "Sent"
                                       join f in dbContext.Api_File_Uploads on c.case_id equals f.CaseId into gj
                                       from subF in gj.DefaultIfEmpty()
                                       where subF == null 
                                       select c).ToList();

                var fileUploadFirstRemainder = (from c in dbContext.Companies
                                       where c.CompanyStatus == "File Upload" && c.UploadEnrollReminder1 != "Sent" && c.UploadEnrollEmailDate <= now.AddDays(-3)
                                                join f in dbContext.Api_File_Uploads on c.case_id equals f.CaseId into gj
                                       from subF in gj.DefaultIfEmpty()
                                       where subF == null
                                       select c).ToList();

                var fileUploadSecondRemainder = (from c in dbContext.Companies
                                                where c.CompanyStatus == "File Upload" && c.UploadEnrollReminder2 != "Sent" && c.UploadEnrollReminder1Date <= now.AddDays(-3)
                                                join f in dbContext.Api_File_Uploads on c.case_id equals f.CaseId into gj
                                                from subF in gj.DefaultIfEmpty()
                                                where subF == null
                                                select c).ToList();

                var fileUploadThirdRemainder = (from c in dbContext.Companies
                                                 where c.CompanyStatus == "File Upload" && c.UploadEnrollReminder3 != "Sent" && c.UploadEnrollReminder3Date <= now.AddDays(-3)
                                                 join f in dbContext.Api_File_Uploads on c.case_id equals f.CaseId into gj
                                                 from subF in gj.DefaultIfEmpty()
                                                 where subF == null
                                                 select c).ToList();

                var completedEmail = dbContext.Companies
                  .Where(c => c.CompanyStatus == "Completed" && c.IsEmailSent != "Sent")
                  .ToList();


                foreach (var company in completedEmail)
                {
                    await SendReminderEmail(company, "CompleteEmail.html", "Action Needed: You Have New Clarity Benefit Solutions Alerts - New Implementation", emailService, dbContext, stoppingToken);
                    company.IsEmailSent = "Sent";
                    
                    dbContext.Companies.Update(company);
                }

                foreach (var company in companiesForFirstReminder)
                {
                    await SendReminderEmail(company, "EmailCopy2.html", "Reminder: It's Time to Begin Your Clarity Plan(s) Implementation - New Implementation", emailService, dbContext, stoppingToken);
                    company.Reminder1 = "Sent";
                    company.Reminder1SentDate = now;
                    dbContext.Companies.Update(company);
                }

                foreach (var company in companiesForSecondReminder)
                {
                    await SendReminderEmail(company, "EmailCopy3.html", "Reminder: It's Time to Begin Your Clarity Plan(s) Implementation - New Implementation", emailService, dbContext, stoppingToken);
                    company.Reminder2 = "Sent";
                    company.Reminder2SentDate = now;
                    dbContext.Companies.Update(company);
                }

                foreach (var company in companiesForThirdReminder)
                {
                    await SendReminderEmail(company, "EmailCopy4.html", "Action Needed|Don’t Forget to Complete Your Clarity Plan(s) Implementation - New Implementation", emailService, dbContext, stoppingToken);
                    company.Reminder3 = "Sent";
                    company.Reminder3SentDate = now;
                    dbContext.Companies.Update(company);
                }

                foreach (var company in fileUploadEmail)
                {
                    await SendReminderEmail(company, "FileUploadEmail.html", "Action Needed: Upload Enrollment Files - New Implementation", emailService, dbContext, stoppingToken);
                    company.UploadEnrollEmail = "Sent";
                    company.UploadEnrollEmailDate = now;
                    dbContext.Companies.Update(company);
                }

                foreach (var company in fileUploadFirstRemainder)
                {
                    await SendReminderEmail(company, "FileUploadRemainder1.html", "Reminder, Action Needed: Upload Enrollment Files - New Implementation", emailService, dbContext, stoppingToken);
                    company.UploadEnrollReminder1 = "Sent";
                    company.UploadEnrollReminder1Date = now;
                    dbContext.Companies.Update(company);
                }

                foreach (var company in fileUploadSecondRemainder)
                {
                    await SendReminderEmail(company, "FileUploadRemainder2.html", "Reminder, Action Needed: Upload Enrollment Files - New Implementation", emailService, dbContext, stoppingToken);
                    company.UploadEnrollReminder2 = "Sent";
                    company.UploadEnrollReminder2Date = now;
                    dbContext.Companies.Update(company);
                }

                foreach (var company in fileUploadThirdRemainder)
                {
                    await SendReminderEmail(company, "FileUploadRemainder3.html", "Final Reminder, Action Needed: Upload Enrollment Files - New Implementation", emailService, dbContext, stoppingToken);
                    company.UploadEnrollReminder3 = "Sent";
                    company.UploadEnrollReminder3Date = now;
                    dbContext.Companies.Update(company);
                }

                await dbContext.SaveChangesAsync(stoppingToken);
            }
        }

        private async Task SendReminderEmail(Company company, string template, string subject, EmailService emailService, ClarityDbContext dbContext, CancellationToken stoppingToken)
        {
            var companyDetails = dbContext.CompanyDetails.FirstOrDefault(cd => cd.CompanyId == company.Id);

            if (company != null && companyDetails != null)
            {
                var emailModel = new Email
                {
                    ToEmail = "donotreply@claritybenefitsolution.com",
                    Subject = subject,
                    CompanyName = companyDetails.Name,
                    FirstName = company.case_implementation_contact_first_name,
                    LastName = company.case_implementation_contact_last_name,
                    CaseOwnerFirstName = company.CaseOwnerName,
                    CaseOwnerLastName = "",
                    CaseOwnerEmail = company.CaseOwnerEmail,
                    Template = template,
                    CaseID = company.case_id
                };

                var htmlFilePath = $"Templates/{emailModel.Template}";

                if (System.IO.File.Exists(htmlFilePath))
                {
                    emailModel.Body = await System.IO.File.ReadAllTextAsync(htmlFilePath, stoppingToken);
                    emailModel.EmailSentdate = DateTime.Now;

                    var replacements = new Dictionary<string, string>
                    {
                        { "{{RecipientName}}", emailModel.RecipientName },
                        { "{{company}}", emailModel.CompanyName },
                        { "{{is_name}}", $"{emailModel.CaseOwnerFirstName} {emailModel.CaseOwnerLastName}" },
                        { "{{is_position_title}}", "Client Engagement Manager" },
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
                        await emailService.SendEmailAsync(emailModel);
                        _logger.LogInformation($"Email sent to {emailModel.FirstName} {emailModel.LastName} at {DateTimeOffset.Now}");
                        
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Failed to send email: {ex.Message}");
                        
                    }
                    finally
                    {
                        dbContext.Api_Notification_Logs.Add(emailLog);
                        
                    }
                }
            }
        }
    }
}
