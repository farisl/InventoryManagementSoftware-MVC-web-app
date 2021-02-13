using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Brand;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IBrandService : IBaseService<Brand>
    {
        void Add(DTOBrand dtoBrand);
        void Edit(DTOBrand dtoBrand);
        IEnumerable<DTOBrand> GetAllDto();
        DTOBrand GetByIdDto(int id);
        Task<IEnumerable<DTODropdown>> GetDropdownItems();
    }
}
