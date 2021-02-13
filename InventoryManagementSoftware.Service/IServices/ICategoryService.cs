using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Category;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface ICategoryService : IBaseService<Category>
    {
        void Add(DTOCategory dtoCategory);
        void Edit(DTOCategory dtoCategory);
        IEnumerable<DTOCategory> GetAllDto();
        DTOCategory GetByIdDto(int id);
        Task<IEnumerable<DTODropdown>> GetDropdownItemsByBrandId(int brandId);
    }
}
