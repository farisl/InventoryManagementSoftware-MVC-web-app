using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Department;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IDepartmentService : IBaseService<Department>
    {
        IEnumerable<Department> GetByInventoryId(int id);
        IEnumerable<DTODepartment> GetByInventoryIdDto(int id);
        IEnumerable<DTODepartment> GetAllDto();
        DTODepartment GetByIdDto(int id);
        Task<IEnumerable<DTODropdown>> GetDropdownItems();
        Task<IEnumerable<DTODropdown>> GetDropdownItemsByInventoryId(int inventoryId);
    }
}
