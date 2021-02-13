using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Web.Helpers
{
    public static class Localization
    {
        public static IList<CultureInfo> SupportedCultures => new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("bs-Latn-BA")
        };

        public static readonly string CurrentCultureCookieName = "IMS.Culture.CurrentCulture";

        public static void UseLocalization(this IApplicationBuilder app)
        {
            RequestLocalizationOptions options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("bs-Latn-BA"),
                SupportedCultures = Localization.SupportedCultures,
                SupportedUICultures = Localization.SupportedCultures
            };

            options.RequestCultureProviders.OfType<CookieRequestCultureProvider>().First().CookieName = CurrentCultureCookieName;

            app.UseRequestLocalization(options);
        }

        public static void SetCurrentCulture(this IResponseCookies cookies, RequestCulture culture)
        {
            cookies.Delete(CurrentCultureCookieName);
            cookies.Append(CurrentCultureCookieName, CookieRequestCultureProvider.MakeCookieValue(culture), new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.MaxValue,
                IsEssential = true
            });
        }
    }
}
