using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Supplier;
using System.Collections.Generic;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface ISupplierService : IBaseService<Supplier>
    {
        void Add(DTOSupplier model);
        void Edit(DTOSupplier model);
        IEnumerable<City> GetCities();
        IEnumerable<DTOSupplier> GetAllDto(string search = null);
        DTOSupplier GetByIdDto(int id);
    }
}
