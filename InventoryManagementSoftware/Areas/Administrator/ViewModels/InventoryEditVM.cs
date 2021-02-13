using InventoryManagementSoftware.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.ViewModels
{
    public class InventoryEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public int PhoneNumberId { get; set; }
        public string PhoneNumber { get; set; }
        public float Size { get; set; }  
        public int CityId { get; set; }
        public List<SelectListItem> Cities { get; set; }

        public static implicit operator InventoryEditVM(Inventory i)
        {
            return new InventoryEditVM
            {
                Id = i.Id,
                Name = i.Name,
                AddressId = i.AddressId,
                Address = i.Address.Name,
                PhoneNumberId = i.PhoneNumberId,
                PhoneNumber = i.PhoneNumber.Number,
                Size = i.Size,
                CityId = i.Address.CityId
            };
        }

        public static implicit operator Inventory(InventoryEditVM i)
        {
            return new Inventory
            {
                Id=i.Id,
                Name=i.Name,
                AddressId=i.AddressId,
                PhoneNumberId=i.Id,
                Size=i.Size                              
            };
        }

    }
}
