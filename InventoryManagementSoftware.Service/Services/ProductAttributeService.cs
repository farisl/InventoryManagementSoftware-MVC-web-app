using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{
    public class ProductAttributeService : BaseService<ProductAttribute>, IProductAttributeService
    {
        private readonly IMSContext _context;
        public ProductAttributeService(IMSContext context) : base(context)
        {
            _context = context;
        }
    }
}
