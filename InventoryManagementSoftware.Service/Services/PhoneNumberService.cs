using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;

namespace InventoryManagementSoftware.Service.Services
{
    public class PhoneNumberService : BaseService<PhoneNumber>, IPhoneNumberService
    {
        public PhoneNumberService(IMSContext context): base(context)
        { }
    }
}
