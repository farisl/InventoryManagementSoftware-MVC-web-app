using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.User;
using InventoryManagementSoftware.Web.Helpers;
using InventoryManagementSoftware.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSoftware.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IUnitOfWork unitOfWork, IMapper mapper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string ReturnUrl)
        {
            var user = HttpContext.GetCurrentUser();
            if (User.Identity.IsAuthenticated && user != null)
            {
                return Url.IsLocalUrl(ReturnUrl) ? (IActionResult)LocalRedirect(ReturnUrl) : RedirectToAction(nameof(Index), "Home", new { area = user.Area });
            }
            return View(new VMLogin());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(VMLogin login)
        {
            if (!ModelState.IsValid) return View(login);

            var user = await AuthHelper.Login(_unitOfWork, _mapper, _signInManager, _userManager, login.Email, login.Password);
            if (user != null)
            {
                HttpContext.Session.Set(Session.Keys.Login.User, user);
                return RedirectToAction("Index", "Home", new { area = user.Area });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await _signInManager.Logout();
            return RedirectToAction(nameof(Index));
        }
    }
}