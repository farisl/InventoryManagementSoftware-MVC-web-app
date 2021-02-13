using InventoryManagementSoftware.Service.DTO.Shelves;

namespace InventoryManagementSoftware.Service.DTO.Product
{
    public class DTOProductShelf
    {
        public int Id { get; set; }
        public DTOProduct Product { get; set; }
        public DTOShelves Shelf { get; set; }
        public double Quantity { get; set; }
    }
}
