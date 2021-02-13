using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.ViewModels
{
    public class DepartmentPreviewVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Inventory { get; set; }
        public float Size { get; set; }

        public DepartmentPreviewVM(int id,string name,string inventory, float size)
        {
            Id = id;
            Name = name;
            Inventory = inventory;
            Size = size;
        }
    }
}
