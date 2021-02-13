using InventoryManagementSoftware.Service.DTO.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.DTO.Export
{
    public class DTOExportDetail
    {
        public int Id { get; set; }
        public int ExportId { get; set; }
        public DTOExport Export { get; set; }
        public int ProductId { get; set; }
        public DTOProduct Product { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }

    }
}
