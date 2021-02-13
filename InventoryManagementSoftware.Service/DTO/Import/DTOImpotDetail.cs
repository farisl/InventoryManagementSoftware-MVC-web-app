using InventoryManagementSoftware.Service.DTO.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.DTO.Import
{
    public class DTOImpotDetail
    {
        public int Id { get; set; }
        public int ImportId { get; set; }
        public DTOImport Import { get; set; }
        public int ProductId { get; set; }
        public DTOProduct Product { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }

    }
}
