using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Import;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{
    public class ImportService : BaseService<Import>, IImportService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;

        public ImportService(IMSContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(DTOImport model)
        {
            if (model.InventoryId == 0 || model.EmployeeId == 0 || model.SupplierId == 0)
                return;
            Import Import = _mapper.Map<Import>(model);
            List<ImportDetail> ImportDetails = _mapper.Map<List<ImportDetail>>(model.ImportProductList);

            if (model.InventoryId > 0 && model.EmployeeId > 0)
                Import.EmployeeId = model.EmployeeId;
            _context.Add(Import);
            _context.SaveChanges();

            if(ImportDetails != null)
            {
                foreach (var ed in ImportDetails)
                {
                    ed.ImportId = Import.Id;
                    double productPrice = _context.ProductPrices
                        .Where(x => x.ProductId == ed.ProductId && !x.IsDeleted)
                        .FirstOrDefault().Price;
                    ed.Price = ed.Quantity * productPrice - (ed.Quantity * productPrice * ed.Discount / 100);
                }
                _context.AddRange(ImportDetails);
                _context.SaveChanges();
            }

        }

        public void Edit(DTOImport model)
        {
            Import Import = _mapper.Map<Import>(model);
            List<ImportDetail> newDetails = _mapper.Map<List<ImportDetail>>(model.ImportProductList);
            List<ImportDetail> oldDetails = _context.ImportDetails
                .Where(x => x.ImportId == model.Id).ToList();
            _context.Update(Import);

            if (newDetails != null && newDetails.Count > 0)
            {
                _context.RemoveRange(oldDetails);
                if (newDetails != null)
                {
                    foreach (var nd in newDetails)
                    {
                        nd.ImportId = Import.Id;
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

        public override bool Delete(Import Import)
        {
            List<ImportDetail> details = _context.ImportDetails
                .Where(x => x.ImportId == Import.Id).ToList();
            _context.RemoveRange(details);

            return base.Delete(Import);
        }

        public IEnumerable<DTOImport> GetAllDto(string search = null, DateTime? date = null)
        {
            var imports = _context.Imports
                .Include(x => x.Supplier)
                .Include(x => x.Employee)
                .Include(x => x.Inventory);

            if (!string.IsNullOrEmpty(search) && date != null)
            {
                imports = _context.Imports
                .Where(x => x.Inventory.Name.Contains(search) && x.Date == date)
                .Include(x => x.Supplier)
                .Include(x => x.Employee)
                .Include(x => x.Inventory);
            }
            else if (date != null)
            {
                imports = _context.Imports
                .Where(x => x.Date == date)
                .Include(x => x.Supplier)
                .Include(x => x.Employee)
                .Include(x => x.Inventory);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                imports = _context.Imports
                   .Where(x => x.Inventory.Name.Contains(search))
                   .Include(x => x.Supplier)
                   .Include(x => x.Employee)
                   .Include(x => x.Inventory);

            }

            return _mapper.Map<IEnumerable<DTOImport>>(imports);
        }

        public DTOImport GetByIdDto(int id)
        {
            Import Import = _context.Imports
                .Where(x => x.Id == id)
                .Include(x => x.Employee)
                .Include(x => x.Supplier)
                .Include(x => x.Inventory)
                .FirstOrDefault();
            return _mapper.Map<DTOImport>(Import);
        }

        public override Import GetById(int id)
        {
            return _context.Imports.
                Include(x => x.Employee)
                .Include(x => x.Inventory)
                .Include(x => x.Supplier).FirstOrDefault(x => x.Id == id);
                
        }

        public int GetImportQuantity(int id)
        {
            return (int)_context.ImportDetails.Where(x => x.ImportId == id)
                .Sum(x => x.Quantity);
        }

        public double GetImportPrice(int id)
        {
            return Math.Round(_context.ImportDetails.Where(x => x.ImportId == id)
                .Sum(x => x.Price), 2);
        }

        public List<ImportDetail> GetImportDetails(int importtId)
        {
            return _context.ImportDetails
                .Where(x => x.ImportId == importtId)
                .ToList();
        }

        public DTOImport GetDetailsByIdDto(int ImportId)
        {
            Import Import = _context.Imports
                .Include(x => x.Inventory)
                .FirstOrDefault(x => x.Id == ImportId);
            if (Import == null)
                return null;

            DTOImport model = _mapper.Map<DTOImport>(Import);
            model.ImportDetails = _context.ImportDetails
                .Where(x => x.ImportId == ImportId)
                .Include(x => x.Product).ToList();

            model.Quantity = (int)model.ImportDetails.Sum(x => x.Quantity);
            model.TotalPrice = model.ImportDetails.Sum(x => x.Price);

            return model;
        }
    }
}
