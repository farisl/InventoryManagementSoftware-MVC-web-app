using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.DTO.User
{
    public class DTOUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Area { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
    }
}
