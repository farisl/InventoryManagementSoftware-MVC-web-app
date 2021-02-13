using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{
    public class ProductShelfService : BaseService<ProductShelf> , IProductShelfService
    {
        private readonly IMSContext _context;
        public ProductShelfService(IMSContext context) : base(context)
        {
            _context = context;
        }
    }
}
