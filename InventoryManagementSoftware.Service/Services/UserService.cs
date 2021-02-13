using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.Services
{
    class UserService : IUserService
    {
        private readonly IMSContext _context;
        public UserService(IMSContext context)
        {
            _context = context;
        }

        public Task<ApplicationUser> FindByPersonId(int pPersonId)
        {
            return _context.Persons.Where(x => x.Id == pPersonId && !x.IsDeleted).Select(x => x.IdentityUser).FirstOrDefaultAsync();
        }

        public Task<ApplicationUser> GetSessionUser(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public Task<ApplicationUser> GetApplicationUserById(int id)
        {
            return _context.Users.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<ApplicationUser> FindByUserNameOrEmail(string name)
        {
            return _context.Users.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.UserName == name || x.Email == name);
        }
    }
}
