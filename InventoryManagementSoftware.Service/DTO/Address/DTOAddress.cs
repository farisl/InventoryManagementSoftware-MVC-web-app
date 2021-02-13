using InventoryManagementSoftware.Service.DTO.City;

namespace InventoryManagementSoftware.Service.DTO.Address
{
    public class DTOAddress
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DTOCity City { get; set; }
    }
}
