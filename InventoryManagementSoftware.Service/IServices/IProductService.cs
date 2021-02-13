using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.DTO.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IProductService : IBaseService<Product>
    {
        IEnumerable<DTOProduct> GetAllDto(string search = null);
        void Add(DTOProduct model);
        bool Edit(DTOProduct model);
        DTOProduct GetDetailsByIdDto(int productId);
        DTOProduct GetByIdDto(int id);
        bool SoftDelete(int productId);
        Task<IEnumerable<DTODropdown>> GetDropdownItems();

    }
}
