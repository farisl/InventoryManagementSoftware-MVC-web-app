using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.ViewModels
{
    public class DepartmentEditVM
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public List<SelectListItem> Inventories { get; set; }
        public string Name { get; set; }
        public float Size { get; set; }
               
    }
}
