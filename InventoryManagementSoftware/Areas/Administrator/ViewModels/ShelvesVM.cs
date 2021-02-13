
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.ViewModels
{
    public class ShelvesVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RowNumber { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<SelectListItem> Departments { get; set; }

        public int InventoryId { get; set; }
        public string InventoryName { get; set; }
        public List<SelectListItem> Inventories { get; set; }

    }
}
