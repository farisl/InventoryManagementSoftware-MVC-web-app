using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface ICustomerService : IBaseService<Customer>
    {
        void Add(DTOCustomer model);
        void Edit(DTOCustomer model);
        IEnumerable<DTOCustomer> GetAllDto(string search = null);
        DTOCustomer GetByIdDto(int id);
        IEnumerable<City> GetCities();
    }
}
