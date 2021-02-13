using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.DTO.Shelves;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IShelvesService : IBaseService<Shelves>
    {
        IEnumerable<Shelves> GetByDepartmentId(int id);
        IEnumerable<DTOShelves> GetByDepartmentIdDto(int id);
        IEnumerable<DTOShelves> GetAllDto();
        DTOShelves GetByIdDto(int id);
        Task<IEnumerable<DTODropdown>> GetDropdownItems();
        Task<IEnumerable<DTODropdown>> GetDropdownItemsByDepartmentId(int departmentId);
    }
}
