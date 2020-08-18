using BonusApp.Pages.ClientPages;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

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
                PdfDocument doc = PdfReader.Open(path, PdfDocumentOpenMode.Modify);
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                // Get the fields And Fill form fields' values
                doc.AcroForm.Fields["IDBONO"].Value = new PdfString(CouponTRCId);
                if (doc.AcroForm.Elements.ContainsKey("/NeedAppearances"))
                {
                    doc.AcroForm.Elements["/ NeedAppearances"] = new PdfBoolean(true);
                }
                else
                {
                    doc.AcroForm.Elements.Add("/NeedAppearances", new PdfBoolean(true));
                }

                path = $"{BaseDir}/generated/{CouponTRCId}.pdf";

                // Save updated document to a MemoryStream type variable
                doc.Save(stream);
            }
            catch (Exception)
            {
                return null;
            }

            return stream;
        }
    }
}