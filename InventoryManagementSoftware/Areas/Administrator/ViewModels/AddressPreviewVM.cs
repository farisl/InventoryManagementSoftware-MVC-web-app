using InventoryManagementSoftware.Core.Models;
using System.Collections.Generic;

namespace InventoryManagementSoftware.ViewModels
{
    public class AddressPreviewVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }


        public static implicit operator AddressPreviewVM(Address a)
        {
            return new AddressPreviewVM
            {
                Id = a.Id,
                Name = a.Name,
                City = a.City.Name
            };
        }
      
    }


}
