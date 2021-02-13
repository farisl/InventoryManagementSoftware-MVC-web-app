using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSoftware.Core.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey ("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [ForeignKey ("PhoneNumber")]
        public int PhoneNumberId { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        public float Size { get; set; }
    }
}
