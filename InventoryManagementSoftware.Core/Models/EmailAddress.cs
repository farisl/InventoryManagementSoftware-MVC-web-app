using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSoftware.Core.Models
{
    public class EmailAddress
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
