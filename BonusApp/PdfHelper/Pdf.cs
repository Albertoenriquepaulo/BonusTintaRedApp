using Aspose.Pdf;
using Aspose.Pdf.Forms;
using BonusApp.Pages.ClientPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.PdfHelper
{
    public class Pdf
    {
        public string CouponType { get; set; }
        public string CouponTRCId { get; set; }
        public string BaseDir { get; set; }
        public string PdfTemplate
        {
            get
            {
                string pdfName = null;
                switch (CouponType)
                {
                    case CouponTypes.BYN100:
                        pdfName = Templates.BYN100;
                        break;
                    case CouponTypes.BYN200:
                        pdfName = Templates.BYN200;
                        break;
                    case CouponTypes.BYN500:
                        pdfName = Templates.BYN500;
                        break;
                    case CouponTypes.COLOR50:
                        pdfName = Templates.COLOR50;
                        break;
                    case CouponTypes.COLOR150:
                        pdfName = Templates.COLOR150;
                        break;
                    case CouponTypes.COLOR350:
                        pdfName = Templates.COLOR350;
                        break;
                    default:
                        break;
                }
                return pdfName;
            }
        }


        public Pdf(string baseDir, string couponType, string couponTRCId)
        {
            CouponType = couponType;
            CouponTRCId = couponTRCId;
            BaseDir = baseDir;
        }
        public Pdf()
        {
        }

        public MemoryStream Create(string couponType = null, string couponTRCId = null)
        {
            if (couponType != null)
            {
                CouponType = couponType;
            }

            if (couponTRCId != null)
            {
                CouponTRCId = couponTRCId;
            }

            if (CouponType == null || CouponTRCId == null)
            {
                return null;
            }

            string path = $"{BaseDir}/templates/{PdfTemplate}";
            MemoryStream stream = new MemoryStream();

            try
            {
                // Open document
                Document pdfDocument = new Document(path);

                // Get the fields
                TextBoxField textBoxField1 = pdfDocument.Form["IDBONO"] as TextBoxField;

                // Fill form fields' values
                textBoxField1.Value = CouponTRCId;

                path = $"{BaseDir}/generated/{CouponTRCId}.pdf";

                // Save updated document
                //pdfDocument.Save(path);
                pdfDocument.Save(stream);
            }
            catch (Exception)
            {
                return null;
            }

            return stream;

        }
    }
}
