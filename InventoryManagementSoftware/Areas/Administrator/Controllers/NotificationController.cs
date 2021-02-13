using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationController(INotificationService notificationService, UserManager<ApplicationUser> userManager)
        {
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public IActionResult GetNotification()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var notifications = _notificationService.GetUserNotifications(userId);
            return Ok(new { UserNotifications = notifications, Count = notifications.Count });
        }

        public IActionResult ReadNotification(int notificationId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            _notificationService.ReadNotification(notificationId, userId);
            return Ok();
        }
    }
}
