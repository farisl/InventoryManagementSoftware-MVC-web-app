using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{
    public class CityService : BaseService<City>, ICityService
    {
        private readonly IMSContext _context;
        public CityService(IMSContext context) : base(context)
        {
            _context = context;
        }
        public override IEnumerable<City> GetAll()
        {
            return _context.Cities.Include(x => x.Country);
        }
        public override City GetById(int id)
        {
            return _context.Cities.Where(x => x.Id == id).Include(x => x.Country).FirstOrDefault();
        }
    }
}
