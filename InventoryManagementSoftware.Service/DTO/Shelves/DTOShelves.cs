using InventoryManagementSoftware.Service.DTO.Department;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.DTO.Shelves
{
    public class DTOShelves
    {
        public int Id { get; set; }
        public DTODepartment Department { get; set; }
        public string Name { get; set; }
        public int RowNumber { get; set; }

        public List<SelectListItem> Departments { get; set; }
        public List<SelectListItem> Inventories { get; set; }
    }
}
