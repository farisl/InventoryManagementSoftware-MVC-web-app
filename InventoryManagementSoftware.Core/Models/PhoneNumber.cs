using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSoftware.Core.Models
{
    public class PhoneNumber
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
    }
}
