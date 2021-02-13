using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryManagementSoftware.Core.Models
{
    public class ImportDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Import")]
        public int ImportId { get; set; }
        public Import Import { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
    }
}
