using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Core.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
