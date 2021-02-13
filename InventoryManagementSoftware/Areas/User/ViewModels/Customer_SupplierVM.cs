using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.ViewModels
{
    public class Customer_SupplierVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public int PhoneNumberId { get; set; }
        public string PhoneNumber { get; set; }
        public int EmailAddressId { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }

        public List<SelectListItem> Cities { get; set; }
    }
}
