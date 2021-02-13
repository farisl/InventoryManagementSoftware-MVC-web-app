using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSoftware.Core.Models
{
    public class Shelves
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Name { get; set; }
        public int RowNumber { get; set; }

    }
}
