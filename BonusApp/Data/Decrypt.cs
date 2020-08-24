using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class Decrypt
    {
        private string Base64EncodedData { get; set; }

        public Decrypt(string base64EncodedData)
        {
            Base64EncodedData = base64EncodedData;
        }

        public string GetBase64DecodeString()
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(Base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
    }
}
