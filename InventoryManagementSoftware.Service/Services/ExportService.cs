using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Export;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{
    public class ExportService : BaseService<Export>, IExportService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;

        public ExportService(IMSContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(DTOExport model)
        {
            if (model.InventoryId == 0 || model.EmployeeId == 0 || model.CustomerId == 0)
                return;

            Export export = _mapper.Map<Export>(model);
            List<ExportDetail> exportDetails = _mapper.Map<List<ExportDetail>>(model.ExportProductList);

            if (model.InventoryId > 0 && model.EmployeeId > 0)
                export.EmployeeId = model.EmployeeId;
            _context.Add(export);
            _context.SaveChanges();

            if(exportDetails != null)
            {
                foreach (var ed in exportDetails)
                {
                    ed.ExportId = export.Id;
                    double productPrice = _context.ProductPrices
                        .Where(x => x.ProductId == ed.ProductId && !x.IsDeleted)
                        .FirstOrDefault().Price;
                    ed.Price = ed.Quantity * productPrice - (ed.Quantity * productPrice * ed.Discount / 100);
                }
                _context.AddRange(exportDetails);
                _context.SaveChanges();
            }

        }

        public void Edit(DTOExport model)
        {
            Export export = _mapper.Map<Export>(model);
            List<ExportDetail> newDetails = _mapper.Map<List<ExportDetail>>(model.ExportProductList);
            List<ExportDetail> oldDetails = _context.ExportDetails
                .Where(x => x.ExportId == model.Id).ToList();
            _context.Update(export);

            if (newDetails != null && newDetails.Count > 0)
            {
                _context.RemoveRange(oldDetails);
                if (newDetails != null)
                {
                    foreach (var nd in newDetails)
                    {
                        nd.ExportId = export.Id;
                        double productPrice = _context.ProductPrices
                            .Where(x => x.ProductId == nd.ProductId && !x.IsDeleted)
                            .FirstOrDefault().Price;
                        nd.Price = nd.Quantity * productPrice - (nd.Quantity * productPrice * nd.Discount / 100);
                    }
                    _context.AddRange(newDetails);
                }
            }
            _context.SaveChanges();
        }

        public override bool Delete(Export export)
        {
            List<ExportDetail> details = _context.ExportDetails
                .Where(x => x.ExportId == export.Id).ToList();
            _context.RemoveRange(details);

            return base.Delete(export);
        }

        public IEnumerable<DTOExport> GetAllDto(string search = null, DateTime? date = null)
        {
            var exports = _context.Exports
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.Inventory);

            if (!string.IsNullOrEmpty(search) && date != null)
            {
                exports = _context.Exports
                .Where(x => x.Inventory.Name.Contains(search) && x.Date == date)
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.Inventory);
            }
            else if (date != null)
            {
                exports = _context.Exports
                .Where(x => x.Date == date)
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Include(x => x.Inventory);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                exports = _context.Exports
                   .Where(x => x.Inventory.Name.Contains(search))
                   .Include(x => x.Customer)
                   .Include(x => x.Employee)
                   .Include(x => x.Inventory);

            }

            return _mapper.Map<IEnumerable<DTOExport>>(exports);
        }

        public DTOExport GetByIdDto(int id)
        {
            Export export = _context.Exports
                .Where(x => x.Id == id)
                .Include(x => x.Employee)
                .Include(x => x.Customer)
                .Include(x => x.Inventory)
                .FirstOrDefault();
            return _mapper.Map<DTOExport>(export);
        }

        public override Export GetById(int id)
        {
            return _context.Exports.
                Include(x => x.Employee)
                .Include(x => x.Inventory)
                .Include(x => x.Customer).FirstOrDefault(x => x.Id == id);

        }

        public int GetExportQuantity(int id)
        {
            return (int)_context.ExportDetails.Where(x => x.ExportId == id)
                .Sum(x => x.Quantity);
        }

        public double GetExportPrice(int id)
        {
            return Math.Round(_context.ExportDetails.Where(x => x.ExportId == id)
                .Sum(x => x.Price), 2);
        }

        public List<ExportDetail> GetExportDetails(int exprotId)
        {
            return _context.ExportDetails
                .Where(x => x.ExportId == exprotId)
                .ToList();
        }

        public DTOExport GetDetailsByIdDto(int exportId)
        {
            Export export = _context.Exports
                .Include(x => x.Inventory)
                .FirstOrDefault(x => x.Id == exportId);
            if (export == null)
                return null;

            DTOExport model = _mapper.Map<DTOExport>(export);
            model.ExportDetails = _context.ExportDetails
                .Where(x => x.ExportId == exportId)
                .Include(x => x.Product).ToList();

            model.Quantity = (int)model.ExportDetails.Sum(x => x.Quantity);
            model.TotalPrice = model.ExportDetails.Sum(x => x.Price);

            return model;
        }
    }
}
