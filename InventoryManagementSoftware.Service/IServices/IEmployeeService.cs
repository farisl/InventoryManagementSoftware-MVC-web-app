using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.DTO.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        void Add(DTOEmployee model);
        void Edit(DTOEmployee model);
        IEnumerable<DTOEmployee> GetAllDto(string search = null);
        DTOEmployee GetByIdDto(int id);
        IEnumerable<Gender> GetGenders();
        public double GetSalary(int employeeId);
        public Inventory GetInventory(int employeeId);
        Task<IEnumerable<DTODropdown>> GetDropdownItemsByInventoryId(int inventoryId);


    }
}
