using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Web.Helpers
{
    public static class AuthHelper
    {

        public const string User = "CURRENT_USER";
        public static DTOUser GetCurrentUser(this HttpContext context)
        {
            return context.Session.Get<DTOUser>(User);
        }
        public static void SetCurrentUser(this HttpContext context, DTOUser DtoUser)
        {
            context.Session.Set<DTOUser>(User, DtoUser);
        }

        public static async Task<DTOUser> Login(IUnitOfWork unitOfWork, IMapper mapper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, string email, string password)
        {
            ApplicationUser user = await unitOfWork.Users.FindByUserNameOrEmail(email);
            if (user == null)
            {
                return null;
            }

            DTOUser User = mapper.Map<DTOUser>(await unitOfWork.Users.GetApplicationUserById(Convert.ToInt32(user.Id)));

            SignInResult result = await signInManager.CheckPasswordSignInAsync(user, password, true);
            if (result.Succeeded && !result.IsLockedOut && User != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.AddRange((await userManager.GetRolesAsync(user))?.Select(x => new Claim(ClaimTypes.Role, x)));
                claims.AddRange(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Expired, TimeSpan.FromHours(24).ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                });
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await signInManager.SignInAsync(user, null, CookieAuthenticationDefaults.AuthenticationScheme);
                return mapper.Map<DTOUser>(user);
            }
            return null;
        }

        public static async Task Logout(this SignInManager<ApplicationUser> signInManager)
        {
            await signInManager.SignOutAsync();
        }
    }
}
