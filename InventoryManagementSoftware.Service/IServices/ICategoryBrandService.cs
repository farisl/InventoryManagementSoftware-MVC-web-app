using InventoryManagementSoftware.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface ICategoryBrandService : IBaseService<CategoryBrand>
    {
        IEnumerable<CategoryBrand> GetAllByBrandId(int brandId);
    }
}
