using InventoryManagementSoftware.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace InventoryManagementSoftware.ViewModels
{
    public class CityEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public static implicit operator CityEditVM(City city)
        {
            return new CityEditVM
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId          
            };
        }


        public static implicit operator City(CityEditVM city)
        {
            return new City
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId
            };
        }
    }
}
