using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using AWSSDK;
using Amazon.SimpleEmailV2.Model;
using Amazon.SimpleEmailV2;
using Newtonsoft.Json.Linq;
using CDPHE.H20.Data.Context;
using CDPHE.H20.Data.Queries;
using Dapper;

namespace CDPHE.H20.Services
{
    // private readonly IAmazonSimpleEmailService _amazonSimpleEmailService;

    public interface IEmailService
    {
        public Task<string> SendEmailAsync(List<string> toAddresses, string bodyHtml, string bodyText, string subject);
        public Task<string> SendLoginEmail(string email, string token);
        public Task<string> SendEmailForNewPlan(int requestId);
        public Task<string> SendEmailForPlanAccepted(int requestId);
        public Task<string> SendEmailForModificationsRequested(int requestId);
        public Task<string> SendEmailForPlanResent(int requestId);
        public Task<string> SendEmailForPlanApproved(int requestId);
        public Task<string> SendEmailForPlanCompleted(int requestId);
        public Task<string> SendEmailForPlanInComplete(int requestId);
        public Task<string> SendEmailForPlanPaid(int requestId);
    }

    public class EmailService : IEmailService
    {
        private readonly DapperContext _dbContext = new DapperContext();
        public EmailService() { }

        public async Task<string> SendEmailAsync(List<string> toAddresses, string bodyHtml, string bodyText, string subject)
        {
            string accessKey = "AKIAUGNZNL4SKXYKFY6Y";
            string secretKey = "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";
            string msg = "{ Success }";

            // Create an instance of the Amazon Simple Email Service V2 client
            var amazonSesV2Client = new AmazonSimpleEmailServiceV2Client(accessKey, secretKey, Amazon.RegionEndpoint.USEast1);

            // Create a new SendEmailRequest with the provided parameters
            // Docs: https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/csharp_ses_code_examples.html
            var sendEmailRequest = new SendEmailRequest
            {
                FromEmailAddress = "info@contactestablished.com",
                // Destination = new Destination { ToAddresses = { "mwilson@istonish.com" } },
                Destination = new Destination { ToAddresses = toAddresses },
                Content = new EmailContent
                {
                    Simple = new Message
                    {
                        Subject = new Content
                        {
                            Charset = "UTF-8",
                            Data = subject
                        },
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = bodyHtml
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = bodyText
                            }
                        },
                    }
                }
            };

            // Send the email using the Amazon SES client
            try
            {
                var response = await amazonSesV2Client.SendEmailAsync(sendEmailRequest);
            }
            catch (Exception ex)
            {
                msg = "{ Failed }";
            }

            return msg;
        }

        public async Task<string> SendLoginEmail(string email, string token)
        {
            List<string> emails = new List<string>();
            emails.Add(email);

            var emailBody = EmailBody(token);
            var response = await SendEmailAsync(emails, emailBody, token, "⚡Quick Login");

            return "true";
        }

        public async Task<string> SendEmailForNewPlan(int requestId)
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails = await GetDetails(requestId);
            List<string> emails = new List<string>();
            string emailTemplate = EmailTemplate();
            string customContent = @"
                    <p>. Please log into the portal to complete this task.</p>";
            string emailBody = emailTemplate.Replace("###CONTENT###", customContent);
            emails.Add(contactDetails.ProviderEmail);
            var response = await SendEmailAsync(emails, emailBody, emailBody, "A remediation plan has been submitted.");
            return "true";
        }

        /// <summary>
        /// The provider accepts the plan sent by the employee
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>Call to email with details</returns>
        public async Task<string> SendEmailForPlanAccepted(int requestId)
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails = await GetDetails(requestId);
            List<string> emails = new List<string>();
            string emailTemplate = EmailTemplate();
            string customContent = @"
                    <p>. Please log into the portal to complete this task.""</p>";
            string emailBody = emailTemplate.Replace("###CONTENT###", customContent);
            emails.Add(contactDetails.EmployeeEmail);
            var response = await SendEmailAsync(emails, emailBody, emailBody, "A remediation plan has been submitted.");
            return "true";
        }

        /// <summary>
        /// The provider requests modifications to the plan.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>Call to email with details</returns>
        public async Task<string> SendEmailForModificationsRequested(int requestId)
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails = await GetDetails(requestId);
            List<string> emails = new List<string>();
            string emailTemplate = EmailTemplate();
            string customContent = @"
                    <p>. Please log into the portal to complete this task.""</p>";
            string emailBody = emailTemplate.Replace("###CONTENT###", customContent);
            emails.Add(contactDetails.EmployeeEmail);
            var response = await SendEmailAsync(emails, emailBody, emailBody, "A remediation plan has been submitted.");
            return "true";
        }

        /// <summary>
        /// The employee resends the plan to the provider adding notes after modifications were requested.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>Call to email with details</returns>
        public async Task<string> SendEmailForPlanResent(int requestId)
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails = await GetDetails(requestId);
            List<string> emails = new List<string>();
            string emailTemplate = EmailTemplate();
            string customContent = @"
                    <p>. Please log into the portal to complete this task.""</p>";
            string emailBody = emailTemplate.Replace("###CONTENT###", customContent);
            emails.Add(contactDetails.ProviderEmail);
            var response = await SendEmailAsync(emails, emailBody, emailBody, "A remediation plan has been submitted.");
            return "true";
        }

        /// <summary>
        /// The employee submits the plan for approval. Fiscal team is notified.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>Call to email with details</returns>
        public async Task<string> SendEmailForPlanApproved(int requestId)
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails = await GetDetails(requestId);
            List<string> emails = new List<string>();
            string emailTemplate = EmailTemplate();
            string customContent = @"
                    <p>. Please log into the portal to complete this task.""</p>";
            string emailBody = emailTemplate.Replace("###CONTENT###", customContent);
            emails.Add(contactDetails.ProviderEmail);
            var response = await SendEmailAsync(emails, emailBody, emailBody, "A remediation plan has been submitted.");
            return "true";
        }

        /// <summary>
        /// The provider completes the plan and submits it to the employee with receipts, invoice, and W-9.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>Call to email with details</returns>
        public async Task<string> SendEmailForPlanCompleted(int requestId)
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails = await GetDetails(requestId);
            List<string> emails = new List<string>();
            string emailTemplate = EmailTemplate();
            string customContent = @"
                    <p>. Please log into the portal to complete this task.""</p>";
            string emailBody = emailTemplate.Replace("###CONTENT###", customContent);
            emails.Add(contactDetails.EmployeeEmail);
            var response = await SendEmailAsync(emails, emailBody, emailBody, "A remediation plan has been submitted.");
            return "true";
        }

        /// <summary>
        /// The employee marks the plan as incomplete and notifies the provider to provide additional information.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>Call to email with details</returns>
        public async Task<string> SendEmailForPlanInComplete(int requestId)
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails = await GetDetails(requestId);
            List<string> emails = new List<string>();
            string emailTemplate = EmailTemplate();
            string customContent = @"
                    <p>. Please log into the portal to complete this task.""</p>";
            string emailBody = emailTemplate.Replace("###CONTENT###", customContent);
            emails.Add(contactDetails.ProviderEmail);
            var response = await SendEmailAsync(emails, emailBody, emailBody, "A remediation plan has been submitted.");
            return "true";
        }

        /// <summary>
        /// The employee emails the provider to say that payment has been submitted. Fiscal is notified.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>Call to email with details</returns>
        public async Task<string> SendEmailForPlanPaid(int requestId)
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails = await GetDetails(requestId);
            List<string> emails = new List<string>();
            string emailTemplate = EmailTemplate();
            string customContent = @"
                    <p>. Please log into the portal to complete this task.""</p>";
            string emailBody = emailTemplate.Replace("###CONTENT###", customContent);
            emails.Add(contactDetails.ProviderEmail);
            var response = await SendEmailAsync(emails, emailBody, emailBody, "A remediation plan has been submitted.");
            return "true";
        }

        public string EmailBody(string token)
        {
            string email = @"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Email Template</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        font-size: 14px;
                        line-height: 1.5;
                        color: #333;
                    }
                    .container {
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                    }
                    .logo {
                        display: block;
                        max-width: 100%;
                        height: auto;
                        margin-bottom: 20px;
                    }
                    .subject {
                        font-weight: bold;
                        font-size: 18px;
                        margin-bottom: 10px;
                    }
                    .login-link {
                        display: inline-block;
                        background-color: #007BFF;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        border-radius: 3px;
                    }
                    .footer {
                        margin-top: 20px;
                        font-size: 12px;
                    }
                    .footer a {
                        color: #007BFF;
                        text-decoration: none;
                    }
                </style>
            </head>
            <body>
                <div class=""container"">
                    <img src=""https://cdphe.colorado.gov/sites/cdphe/files/logo.svg"" alt=""Your Company Logo"" class=""logo"">
                    <p class=""subject"">⚡Quick Login : Access Your Account Now!</p>
                    <p>Please use the follow token to login to the website</p>
                    <p style=""font-size:16pt:font-weight:bold;"">" + token + "</p></div></body></html>";

            return email;
        }

        public string EmailTemplate()
        {
            string email = @"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Email Template</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        font-size: 14px;
                        line-height: 1.5;
                        color: #333;
                    }
                    .container {
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                    }
                    .logo {
                        display: block;
                        max-width: 100%;
                        height: auto;
                        margin-bottom: 20px;
                    }
                    .subject {
                        font-weight: bold;
                        font-size: 18px;
                        margin-bottom: 10px;
                    }
                    .login-link {
                        display: inline-block;
                        background-color: #007BFF;
                        color: #fff;
                        padding: 10px 20px;
                        text-decoration: none;
                        border-radius: 3px;
                    }
                    .footer {
                        margin-top: 20px;
                        font-size: 12px;
                    }
                    .footer a {
                        color: #007BFF;
                        text-decoration: none;
                    }
                </style>
            </head>
            <body>
                <div class=""container"">
                    <img src=""https://cdphe.colorado.gov/sites/cdphe/files/logo.svg"" alt=""CDPHE Logo"" class=""logo"">
                    ###CONTENT###";

            return email;
        }

        public async Task<ContactDetails> GetDetails(int requestId)
        {
            var query = EmailQuery.GetContactDetails();
            
            using (var connection = _dbContext.CreateConnection())
            {
                var response = await connection.QueryFirstOrDefaultAsync<ContactDetails>(query, new { RequestIdValue = requestId });
                return response;
            }
        }

        public class ContactDetails
        {
            public string EmployeeName { get; set; }
            public string ProviderName { get; set; }
            public string EmployeeEmail { get; set; }
            public string ProviderEmail { get; set; }
            public string FacilityName { get; set; }
        }
    } 
}
