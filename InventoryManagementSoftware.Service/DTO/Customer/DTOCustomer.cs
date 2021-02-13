using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Address;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace InventoryManagementSoftware.Service.DTO.Customer
{
    public class DTOCustomer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DTOAddress Address { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public EmailAddress EmailAddress { get; set; }
                
    }
}
