using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryManagementSoftware.Core.Models
{
    public class NotificationApplicationUser
    {
        [Key]
        public int Id { get; set; }
        public bool IsRead { get; set; }

        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
