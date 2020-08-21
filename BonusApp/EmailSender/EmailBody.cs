using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BonusApp.EmailSender
{
    public class EmailBody
    {
        public string Body { get; set; } = string.Empty;

        public bool CreateBody(string TRCId, int couponPages, string colorCouponType)
        {
            try
            {
                using (StreamReader reader = new StreamReader("pdfs/templates/EmailTemplate.html"))
                {
                    Body = reader.ReadToEnd();
                }

                Body = Replace("TRCId", TRCId, Body);
                Body = Replace("couponPages", couponPages.ToString(), Body);
                Body = Replace("colorCouponType", colorCouponType, Body);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private string Replace(string pattern, string replace, string input)
        {
            pattern = @$"\b{pattern}\b";
            return Regex.Replace(input, pattern, replace);
        }

    }
}
