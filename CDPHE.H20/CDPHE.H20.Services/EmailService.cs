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

namespace CDPHE.H20.Services
{
    // private readonly IAmazonSimpleEmailService _amazonSimpleEmailService;
    
    public interface IEmailService
    {

    }

    public class EmailService : IEmailService
    {
        public EmailService() { }

        public async Task<string> SendEmailAsync(List<string> toAddresses,
        string bodyHtml, string bodyText, string subject)
        {
            string accessKey = "";
            string secretKey = "";
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
                    <p>" + token + "</p></div></body></html>```";

            return email;
        }
    }
}
