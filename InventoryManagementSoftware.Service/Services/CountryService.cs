using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{    
    public class CountryService : BaseService<Country>, ICountryService
    {
        
        public CountryService(IMSContext context) : base(context)
        {
        }

    }
}
