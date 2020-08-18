using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BonusApp.EmailSender
{
    public class EmailHelper
    {
        //https://myaccount.google.com/lesssecureapps
        private IConfiguration _configuration;
        public int GmailData { get; set; }

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendStreamByEmail(string to, string subject, string body, string attachmentFileName, MemoryStream stream)
        {
            if (stream == null)
            {
                return false;
            }

            GmailData gmailData = new GmailData
            {
                Host = _configuration["Gmail:Host"],
                Port = int.Parse(_configuration["Gmail:Port"]),
                UserName = _configuration["Gmail:UserName"],
                Password = _configuration["Gmail:Password"],
                Enable = bool.Parse(_configuration["Gmail:SMTP:starttls:enable"]),
            };

            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = gmailData.Host,
                    Port = gmailData.Port,
                    EnableSsl = gmailData.Enable,
                    Credentials = new NetworkCredential(gmailData.UserName, gmailData.Password)
                };

                var mailMessage = new MailMessage(gmailData.UserName, to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                stream.Position = 0;
                ContentType contentType = new ContentType(MediaTypeNames.Application.Pdf);
                Attachment reportAttachment = new Attachment(stream, contentType);
                reportAttachment.ContentDisposition.FileName = $"{attachmentFileName}.pdf";
                mailMessage.Attachments.Add(reportAttachment);


                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
        }
    }
}
