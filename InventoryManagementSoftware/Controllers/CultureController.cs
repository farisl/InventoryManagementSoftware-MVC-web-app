using InventoryManagementSoftware.Web.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace InventoryManagementSoftware.Web.Controllers
{
    public class CultureController : Controller
    {
        private readonly IOptions<RequestLocalizationOptions> options;

        public CultureController(IOptions<RequestLocalizationOptions> options)
        {
            this.options = options;
        }

        public IActionResult SetLanguage(string culture, string returnUrl = null)
        {
            Response.Cookies.SetCurrentCulture(new RequestCulture(culture ?? CultureInfo.CurrentUICulture.Name));
            return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl) ? LocalRedirect(returnUrl) : LocalRedirect("~/");
        }
    }
}