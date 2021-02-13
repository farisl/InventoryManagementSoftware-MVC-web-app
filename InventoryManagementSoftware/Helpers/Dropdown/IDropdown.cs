using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Web.Helpers.Dropdown
{
    public interface IDropdown
    {
        Task<IEnumerable<SelectListItem>> Attributes();

        #region CategoryBrand
        Task<IEnumerable<SelectListItem>> Brands();
        Task<IEnumerable<SelectListItem>> CategoriesByBrandId(int brandId);
        #endregion

        #region Inventory
        Task<IEnumerable<SelectListItem>> Inventories();
        Task<IEnumerable<SelectListItem>> Departments();
        Task<IEnumerable<SelectListItem>> DepartmentsByInventoryId(int inventoryId);
        Task<IEnumerable<SelectListItem>> Shelves();
        Task<IEnumerable<SelectListItem>> ShelvesByDepartmentId(int departmentId);
        #endregion

        #region Export
        Task<IEnumerable<SelectListItem>> EmployeesByInventoryId(int inventoryId);
        Task<IEnumerable<SelectListItem>> Products();

        #endregion
    }
}
