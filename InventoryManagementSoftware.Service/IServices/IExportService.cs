using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Export;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IExportService : IBaseService<Export>
    {
        void Add(DTOExport model);
        void Edit(DTOExport model);
        IEnumerable<DTOExport> GetAllDto(string search = null, DateTime? date = null);
        DTOExport GetByIdDto(int id);
        public int GetExportQuantity(int id);
        public double GetExportPrice(int id);
        public List<ExportDetail> GetExportDetails(int exprotId);
        public DTOExport GetDetailsByIdDto(int exportId);


    }
}
