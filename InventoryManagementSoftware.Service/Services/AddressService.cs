using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{
    public class AddressService :  BaseService<Address>, IAddressService
    {
        private readonly IMSContext _context;
        public AddressService(IMSContext context) : base(context)
        {
            _context = context;
        }
        public override IEnumerable<Address> GetAll()
        {
            return _context.Addresses.Include(x => x.City).ThenInclude(x => x.Country);
        }
        public override Address GetById(int id)
        {
            return _context.Addresses.Include(x => x.City).ThenInclude(x => x.Country).Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
