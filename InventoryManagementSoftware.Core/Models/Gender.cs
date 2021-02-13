using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSoftware.Core.Models
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
