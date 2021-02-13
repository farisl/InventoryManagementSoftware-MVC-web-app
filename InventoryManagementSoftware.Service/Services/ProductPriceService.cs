using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;

namespace InventoryManagementSoftware.Service.Services
{
    public class ProductPriceService : BaseService<ProductPrice>, IProductPriceService
    {
        private readonly IMSContext _context;
        public ProductPriceService(IMSContext context): base(context)
        {
            _context = context;
        }
    }
}
