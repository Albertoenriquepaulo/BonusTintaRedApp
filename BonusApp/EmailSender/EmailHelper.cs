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
        private GmailData GmailData { get; set; }

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            GmailData = new GmailData
            {
                Host = _configuration["Gmail:Host"],
                Port = int.Parse(_configuration["Gmail:Port"]),
                UserName = _configuration["Gmail:UserName"],
                Password = _configuration["Gmail:Password"],
                Enable = bool.Parse(_configuration["Gmail:SMTP:starttls:enable"]),
            };
        }

        public bool SendStreamByEmail(string to, string subject, string body, string attachmentFileName, MemoryStream stream)
        {
            if (stream == null)
            {
                return false;
            }

            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = GmailData.Host,
                    Port = GmailData.Port,
                    EnableSsl = GmailData.Enable,
                    Credentials = new NetworkCredential(GmailData.UserName, GmailData.Password)
                };

                var mailMessage = new MailMessage(GmailData.UserName, to);
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
        public bool SendStreamListByEmail(string to, string subject, string body, List<string> attachmentFileName, List<MemoryStream> streamList)
        {
            if (streamList == null || streamList.Count == 0)
            {
                return false;
            }

            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = GmailData.Host,
                    Port = GmailData.Port,
                    EnableSsl = GmailData.Enable,
                    Credentials = new NetworkCredential(GmailData.UserName, GmailData.Password)
                };

                var mailMessage = new MailMessage(GmailData.UserName, to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                ContentType contentType = new ContentType(MediaTypeNames.Application.Pdf);
                int i = 0;
                foreach (MemoryStream stream in streamList)
                {
                    stream.Position = 0;
                    Attachment reportAttachment = new Attachment(stream, contentType);
                    reportAttachment.ContentDisposition.FileName = $"{attachmentFileName[i++]}.pdf";
                    mailMessage.Attachments.Add(reportAttachment);
                }
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
        }

        public string GetSender()
        {
            return GmailData.UserName;
        }
    }
}
