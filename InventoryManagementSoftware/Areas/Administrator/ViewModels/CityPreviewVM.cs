using InventoryManagementSoftware.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.ViewModels
{
    public class CityPreviewVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public static implicit operator CityPreviewVM(City city)
        {
            return new CityPreviewVM
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country.Name
            };
        }
    }
    
}
