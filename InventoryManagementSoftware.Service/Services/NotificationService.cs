using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.Hubs;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{
    public class NotificationService : BaseService<Notification>, INotificationService
    {
        private readonly IMSContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IMSContext context, IHubContext<NotificationHub> hubContext) : base(context)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public void Create(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();

            foreach (var u in _context.Persons.Where(x => x.IdentityUserId != notification.UserId).ToList())
            {
                var userNotification = new NotificationApplicationUser
                {
                    NotificationId = notification.Id,
                    UserId = u.IdentityUserId,
                    IsRead = false
                };
                _context.UserNotifications.Add(userNotification);
                _context.SaveChanges();
            }

            _hubContext.Clients.All.SendAsync("sendToUser");
        }

        public List<NotificationApplicationUser> GetUserNotifications(string userId)
        {
            return _context.UserNotifications.Where(x => x.UserId == int.Parse(userId) && !x.IsRead)
                .OrderByDescending(x => x.NotificationId)
                .Include(n => n.Notification).ToList();
        }

        public void ReadNotification(int notificationId, string userId)
        {
            var notification = _context.UserNotifications.Where(x => x.NotificationId == notificationId && x.UserId == int.Parse(userId))
                                                        .FirstOrDefault();
            notification.IsRead = true;
            _context.UserNotifications.Update(notification);
            _context.SaveChanges();
        }
    }
}
