using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Attribute;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IAttributeService : IBaseService<Attribute>
    {
        List<DTOAttribute> GetAllDto();
        DTOAttribute GetByIdDto(int id);
        void Add(DTOAttribute model);
        void Edit(DTOAttribute model);
        Task<IEnumerable<DTODropdown>> GetDropdownItems();
    }
}
