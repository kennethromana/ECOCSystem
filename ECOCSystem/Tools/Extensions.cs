using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECOCSystem.Tools
{
    public static class Extensions
    {
        public static string PartialViewToString(this PartialViewResult partialView)
        {
            var httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                throw new NotSupportedException("An HTTP context is required to render the partial view to a string");
            }

            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

            var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;

            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                using (var tw = new HtmlTextWriter(sw))
                {
                    view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, tw), tw);
                }
            }

            return sb.ToString();
        }
        public static string Encrypt(this string input, string privateKey)
        {
            using (var algorithm = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var saltedInput = new Byte[privateKey.Length + inputBytes.Length];

                Encoding.UTF8.GetBytes(privateKey).CopyTo(saltedInput, 0);
                inputBytes.CopyTo(saltedInput, privateKey.Length);

                var hashedBytes = algorithm.ComputeHash(saltedInput);

                return BitConverter.ToString(hashedBytes).Replace("-", "");
            }
        }
    }
}