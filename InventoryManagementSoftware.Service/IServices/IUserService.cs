using InventoryManagementSoftware.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IUserService
    {
        Task<ApplicationUser> FindByPersonId(int pPersonId);
        Task<ApplicationUser> GetSessionUser(int id);
        Task<ApplicationUser> GetApplicationUserById(int id);
        Task<ApplicationUser> FindByUserNameOrEmail(string name);
    }
}
