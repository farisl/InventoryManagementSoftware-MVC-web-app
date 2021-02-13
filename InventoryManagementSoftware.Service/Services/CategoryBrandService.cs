using System.Collections.Generic;
using System.Linq;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSoftware.Service.Services
{
    public class CategoryBrandService : BaseService<CategoryBrand> , ICategoryBrandService
    {
        private readonly IMSContext _context;
        public CategoryBrandService(IMSContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<CategoryBrand> GetAll()
        {
            return _context.CategoriesBrands
                .Include(x=>x.Brand)
                .Include(x=>x.Category);
        }

        public IEnumerable<CategoryBrand> GetAllByBrandId(int brandId)
        {
            return _context.CategoriesBrands.Where(x=>x.BrandId == brandId)
                .Include(x => x.Brand)
                .Include(x => x.Category);
        }
    }
}
