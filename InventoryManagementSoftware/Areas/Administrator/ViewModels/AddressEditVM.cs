using InventoryManagementSoftware.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.ViewModels
{
    public class AddressEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public List<SelectListItem> Cities { get; set; }

        
        public static implicit operator AddressEditVM(Address a)
        {
            return new AddressEditVM
            {
                Id = a.Id,
                Name=a.Name,
                CityId=a.CityId
            };
        }


        public static implicit operator Address(AddressEditVM a)
        {
            return new Address
            {
                Id = a.Id,
                Name = a.Name,
                CityId = a.CityId
            };
        }

    }
}
