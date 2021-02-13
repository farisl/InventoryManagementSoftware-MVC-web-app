using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Import;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IImportService : IBaseService<Import>
    {
        void Add(DTOImport model);
        void Edit(DTOImport model);
        IEnumerable<DTOImport> GetAllDto(string search = null, DateTime? date = null);
        DTOImport GetByIdDto(int id);
        public int GetImportQuantity(int id);
        public double GetImportPrice(int id);
        public List<ImportDetail> GetImportDetails(int importtId);
        public DTOImport GetDetailsByIdDto(int ImportId);

    }
}
