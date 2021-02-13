using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Customer;
using InventoryManagementSoftware.Service.DTO.Employee;
using InventoryManagementSoftware.Service.DTO.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.DTO.Export
{
    public class DTOExport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int InventoryId { get; set; }
        public DTOInventory Inventory { get; set; }
        public int CustomerId { get; set; }
        public DTOCustomer Customer { get; set; }
        public int EmployeeId { get; set; }
        public DTOEmployee Employee { get; set; }
        public List<ExportDetail> ExportDetails { get; set; }
        public List<ExportProductList> ExportProductList { get; set; }

        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        public List<SelectListItem> Inventories { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> Employees { get; set; }

    }

    public class ExportProductList
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public List<SelectListItem> Products { get; set; }
    }
}
