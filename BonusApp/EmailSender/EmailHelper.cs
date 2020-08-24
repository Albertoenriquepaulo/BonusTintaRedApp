using BonusApp.Data;
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
                Password = (new Decrypt(_configuration["Gmail:Password"])).GetBase64DecodeString(),
                Enable = bool.Parse(_configuration["Gmail:SMTP:starttls:enable"])
            };
        }
        public bool SendHtmTemplateListByEmail(string to, List<string> emailSubjectList, List<EmailBody> emailBodies)
        {
            if (emailBodies == null || emailBodies.Count == 0)
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
                //mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                ContentType contentType = new ContentType(MediaTypeNames.Application.Pdf);

                //https://stackoverflow.com/questions/18358534/send-inline-image-in-email
                //https://stackoverflow.com/questions/16442196/email-html-document-embedding-images-using-c-sharp

                mailMessage.Attachments.Add(IncludeImageInlineAttachment("pdfs/templates/image005.png", "image005"));
                //mailMessage.Attachments.Add(IncludeImageInlineAttachment("pdfs/templates/image002.png", "image002"));
                //mailMessage.Attachments.Add(IncludeImageInlineAttachment("pdfs/templates/image003.png", "image003"));
                //mailMessage.Attachments.Add(IncludeImageInlineAttachment("pdfs/templates/image004.png", "image004"));

                //mailMessage.Attachments.Add(IncludeImageInlineAttachment("pdfs/templates/image006.jpg", "image006"));

                int i = 0;
                foreach (var body in emailBodies)
                {
                    mailMessage.Subject = $"Bono de impresion {emailSubjectList[i++]}";
                    mailMessage.Body = body.Body;
                    smtpClient.Send(mailMessage);
                }

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
        public Attachment IncludeImageInlineAttachment(string path, string imageNameInHtml)
        {
            Attachment inlineLogo;
            try
            {
                inlineLogo = new Attachment(path);
                inlineLogo.ContentId = imageNameInHtml;

                inlineLogo.ContentDisposition.Inline = true;
                inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            }
            catch (Exception)
            {
                return null;
            }
            return inlineLogo;
        }
        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
