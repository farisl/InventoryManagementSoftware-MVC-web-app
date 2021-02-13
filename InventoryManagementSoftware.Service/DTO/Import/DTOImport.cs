using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Supplier;
using InventoryManagementSoftware.Service.DTO.Employee;
using InventoryManagementSoftware.Service.DTO.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.DTO.Import
{
    public class DTOImport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int InventoryId { get; set; }
        public DTOInventory Inventory { get; set; }
        public int SupplierId { get; set; }
        public DTOSupplier Supplier { get; set; }
        public int EmployeeId { get; set; }
        public DTOEmployee Employee { get; set; }
        public List<ImportDetail> ImportDetails { get; set; }
        public List<ImportProductList> ImportProductList { get; set; }

        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        public List<SelectListItem> Inventories { get; set; }
        public List<SelectListItem> Suppliers { get; set; }
        public List<SelectListItem> Employees { get; set; }

    }

    public class ImportProductList
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public List<SelectListItem> Products { get; set; }
    }
}
