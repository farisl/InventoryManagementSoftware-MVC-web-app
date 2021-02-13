using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSoftware.Web.ViewModels
{
    public class VMLogin
    {
        [Required(ErrorMessage = "ErrEmail")]
        [StringLength(50, ErrorMessage = "ErrMaxCharEmail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "ErrPassword")]
        [StringLength(20, ErrorMessage = "ErrMaxCharPassword")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
