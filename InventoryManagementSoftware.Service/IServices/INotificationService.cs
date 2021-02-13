using InventoryManagementSoftware.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface INotificationService : IBaseService<Notification>
    {
        void Create(Notification notification);

        List<NotificationApplicationUser> GetUserNotifications(string userId);

        void ReadNotification(int notificationId, string userId);
    }
}
