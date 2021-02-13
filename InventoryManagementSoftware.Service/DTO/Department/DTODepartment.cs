using InventoryManagementSoftware.Service.DTO.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Service.DTO.Department
{
    public class DTODepartment
    {
        public int Id { get; set; }
        public DTOInventory Inventory { get; set; }
        public string Name { get; set; }
        public float Size { get; set; }
        public List<SelectListItem> Inventories { get; set; }       

       
    }
}
