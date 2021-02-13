using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSoftware.Core.Models
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Attribute")]
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public string Value { get; set; }
    }
}
