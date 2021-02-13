using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.DTO.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IInventoryService : IBaseService<Inventory>
    {        
        void Add(DTOInventory inventory);
        void Edit(DTOInventory inventory);
        IEnumerable<DTOInventory> GetAllDto();
        DTOInventory GetByIdDto(int id);
        Task<IEnumerable<DTODropdown>> GetDropdownItems();
    }
}
