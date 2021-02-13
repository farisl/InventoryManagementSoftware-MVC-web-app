using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Address;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.DTO.Supplier
{
    public class DTOSupplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DTOAddress Address { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public EmailAddress EmailAddress { get; set; }
    }
}
