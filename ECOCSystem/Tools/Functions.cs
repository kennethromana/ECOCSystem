using PdfSharp.Drawing;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOCSystem.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace ECOCSystem.Tools
{
    public class Functions
    {
        public static bool FillParamountPolicyCondition(ParamountVehicleType paramountVehicleType, string PolicyNumber, string Location, string Day, string Month, string Year)
        {
            try
            {
                using (PdfSharp.Pdf.PdfDocument outputDocument = new PdfSharp.Pdf.PdfDocument())
                {
                    string ParamountPath = HttpContext.Current.Server.MapPath(string.Format("~/Reports/ParamountFiles/"));
                    string path = HttpContext.Current.Server.MapPath(string.Format("~/Reports/VRTempFiles/"));
                    string pdfFile = "";
                    switch (paramountVehicleType)
                    {
                        case ParamountVehicleType.CV:
                            {
                                pdfFile = "CV.pdf";
                                break;
                            }
                        case ParamountVehicleType.MC:
                            {
                                pdfFile = "MC.pdf";
                                break;
                            }
                        case ParamountVehicleType.PC:
                            {
                                pdfFile = "PC.pdf";
                                break;
                            }
                    }
                    PdfSharp.Pdf.PdfDocument importDocument = PdfSharp.Pdf.IO.PdfReader.Open(ParamountPath + pdfFile, PdfDocumentOpenMode.Modify);

                    if (importDocument.AcroForm.Elements.ContainsKey("/NeedAppearances") == false)
                        importDocument.AcroForm.Elements.Add("/NeedAppearances", new PdfSharp.Pdf.PdfBoolean(true));
                    else importDocument.AcroForm.Elements["/NeedAppearances"] = new PdfSharp.Pdf.PdfBoolean(true);

                    //get field
                    PdfTextField locationJC = (PdfTextField)(importDocument.AcroForm.Fields["Location_JC"]);
                    PdfTextField DayThJC = (PdfTextField)(importDocument.AcroForm.Fields["DayTh_JC"]);
                    PdfTextField MonthJC = (PdfTextField)(importDocument.AcroForm.Fields["Month_JC"]);
                    PdfTextField Year2JC = (PdfTextField)(importDocument.AcroForm.Fields["Year2_JC"]);

                    locationJC.Value = new PdfSharp.Pdf.PdfString(Location);
                    DayThJC.Value = new PdfSharp.Pdf.PdfString(Day);
                    MonthJC.Value = new PdfSharp.Pdf.PdfString(Month);
                    Year2JC.Value = new PdfSharp.Pdf.PdfString(Year);

                    XGraphics gfx = XGraphics.FromPdfPage(importDocument.Pages[0], XGraphicsPdfPageOptions.Prepend);
                    XImage image = XImage.FromFile(ParamountPath + "Signature.png");
                    gfx.DrawImage(image, 430, 730, 100, 100);

                    var paramountQRCode = GetQRParamount(PolicyNumber);

                    if (paramountQRCode.StatusCode == "200")
                    {
                        using (var streamFile = new MemoryStream(paramountQRCode.ResponseData))
                        {
                            XImage byteImage = XImage.FromStream(streamFile);

                            gfx.DrawImage(byteImage, 505, 26, XUnit.FromCentimeter(2), XUnit.FromCentimeter(2));

                        }
                    }



                    importDocument.Save(path + PolicyNumber + "_Attachment.pdf");


                    //Merge together with saved pdf
                    List<string> pdfList = new List<string>();
                    pdfList.Add(path + PolicyNumber + "_Policy.pdf");
                    pdfList.Add(path + PolicyNumber + "_Attachment.pdf");

                    if (MergePDF(path + PolicyNumber + ".pdf", pdfList))
                    {
                        foreach (var pdf in pdfList)
                        {
                            if (File.Exists(pdf))
                                File.Delete(pdf);
                        }
                        return true;
                    }

                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static ParamountQRCodeReturn GetQRParamount(string PolicyNumber)
        {
            ParamountQRCodeReturn returnModel = new ParamountQRCodeReturn();

            using (var client = new HttpClient())
            {
                using (var db = new ECOCEntities())
                {
                    var ParamountServiceURL = db.SystemVariable.Where(o => o.VariableName == "ParamountServiceURL").FirstOrDefault().VariableValue;
                    var ParamountRequestURL = db.SystemVariable.Where(o => o.VariableName == "ParamountQRCodeURL").FirstOrDefault().VariableValue;
                    var ParamountPrivateKey = db.SystemVariable.Where(o => o.VariableName == "ParamountQRCodeKey").FirstOrDefault().VariableValue;
                    //Test COC Insert to Paramount
                    var builder = new UriBuilder(ParamountServiceURL + ParamountRequestURL + PolicyNumber);
                    builder.Port = -1;
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    //query["username"] = ConfigurationManager.AppSettings["ParamountCOCAFUserName"].ToString();

                    builder.Query = query.ToString();
                    string url = builder.ToString();

                    var hashPassword = Tools.Functions.EncryptParamount(ParamountRequestURL + PolicyNumber, ParamountPrivateKey);
                    client.DefaultRequestHeaders.Add("Authentication-Hash", hashPassword);

                    //Post to Paramount
                    var postjob = client.GetAsync(url);
                    var result = postjob.Result;
                    var responseString = result.Content.ReadAsStringAsync();
                    returnModel = JsonConvert.DeserializeObject<ParamountQRCodeReturn>(responseString.Result);

                    return returnModel;
                }
            }

        }
        public static string EncryptParamount(string input, string privateKey)
        {
            using (var algorithm = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input.ToLower());
                var saltedInput = new byte[privateKey.Length + inputBytes.Length];

                Encoding.UTF8.GetBytes(privateKey).CopyTo(saltedInput, 0);
                inputBytes.CopyTo(saltedInput, privateKey.Length);

                var hashedBytes = algorithm.ComputeHash(saltedInput);

                return BitConverter.ToString(hashedBytes).Replace("-", "");
            }
        }
        public static bool MergePDF(string outputString, List<string> pdfFiles)
        {
            try
            {
                PdfSharp.Pdf.PdfDocument outputPDFDocument = new PdfSharp.Pdf.PdfDocument();

                foreach (string pdfFile in pdfFiles)
                {
                    PdfSharp.Pdf.PdfDocument inputPDFDocument = PdfSharp.Pdf.IO.PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);
                    outputPDFDocument.Version = inputPDFDocument.Version;
                    foreach (PdfSharp.Pdf.PdfPage page in inputPDFDocument.Pages)
                    {
                        outputPDFDocument.AddPage(page);
                    }
                }

                outputPDFDocument.Save(outputString);
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }

        public static string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }
    }
}