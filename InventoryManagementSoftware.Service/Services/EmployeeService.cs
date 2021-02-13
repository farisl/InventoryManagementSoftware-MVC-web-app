using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.DTO.Employee;
using InventoryManagementSoftware.Service.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.IServices
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(IMSContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(DTOEmployee model)
        {
            Address address = _mapper.Map<Address>(model.Address);
            Employee employee = _mapper.Map<Employee>(model);

            _context.Addresses.Add(address);
            _context.SaveChanges();

            employee.AddressId = address.Id;
            employee.Username = model.FirstName.ToLower() + model.LastName.Substring(0, 1).ToLower();
            int b = 0;
            foreach (var x in _context.Users.ToList())
            {
                if (x.UserName == employee.Username)
                    b++;
            }
            if (b > 0)
                employee.Username += b.ToString();
            employee.PasswordHash = "AQAAAAEAACcQAAAAEA27GXFgUsI5e3+EHt0MSqROepea6LqlbFSugDckdIrVK+MyaYbiqZABm4qUmjep+A==";
            _context.Add(employee);
            _context.SaveChanges();

            EmployeeInventory employeeInventory = new EmployeeInventory
            {
                EmployeeId = employee.Id,
                InventoryId = model.InventoryId,
                HireDate = null,
                EndDate = null
            };
            if (employeeInventory.InventoryId != null)
                employeeInventory.HireDate = DateTime.Now;
            _context.Add(employeeInventory);
            _context.SaveChanges();

            EmployeeSalaries employeeSalary = new EmployeeSalaries
            {
                EmployeeId = employee.Id,
                Value = model.Salary,
                StartDate = DateTime.Now,
                EndDate = null
            };
            _context.Add(employeeSalary);
            _context.SaveChanges();

            ApplicationUser user = new ApplicationUser
            {
                UserName = employee.Username,
                NormalizedUserName = employee.Username.ToUpper(),
                Email = employee.Username + "@ims.ba",
                NormalizedEmail = (employee.Username + "@ims.ba").ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = employee.PhoneNumber,
                PhoneNumberConfirmed = false,
                PasswordHash = employee.PasswordHash,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            ApplicationUserRole userRole = new ApplicationUserRole
            {
                UserId = user.Id,
                RoleId = 2
            };
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            Person person = new Person
            {
                Active = true,
                CreatedDate = DateTime.Now,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                GenderId = employee.GenderId,
                PhoneNumber = employee.PhoneNumber,
                IdentityUserId = user.Id
            };
            _context.Persons.Add(person);
            _context.SaveChanges();

        }

        public void Edit(DTOEmployee model)
        {
            Address address = _mapper.Map<Address>(model.Address);
            Employee employee = _mapper.Map<Employee>(model);

            _context.Update(address);
            _context.SaveChanges();

            EmployeeInventory employeeInventory = _context.EmployeeInventories
                .Where(x => x.EmployeeId == employee.Id && x.EndDate == null)
                .FirstOrDefault();
            if(employeeInventory.InventoryId == null)
            {
                employeeInventory.InventoryId = model.InventoryId;
                if (employeeInventory.InventoryId != null)
                    employeeInventory.HireDate = DateTime.Now;
            }
            else
            {
                if(employeeInventory.InventoryId != model.InventoryId)
                {
                    employeeInventory.EndDate = DateTime.Now;
                    EmployeeInventory ei = new EmployeeInventory
                    {
                        EmployeeId = employee.Id,
                        InventoryId = model.InventoryId,
                        HireDate = null
                    };
                    if (ei.InventoryId != null)
                        ei.HireDate = DateTime.Now;
                    _context.Add(ei);
                    _context.SaveChanges();
                }
            }

            EmployeeSalaries employeeSalaries = _context.EmployeeSalaries
                .Where(x => x.EmployeeId == employee.Id && x.EndDate == null)
                .FirstOrDefault();
            if(employeeSalaries.Value != model.Salary)
            {
                employeeSalaries.EndDate = DateTime.Now;
                EmployeeSalaries es = new EmployeeSalaries
                {
                    EmployeeId = employee.Id,
                    Value = model.Salary,
                    StartDate = DateTime.Now
                };
                _context.Add(es);
                _context.SaveChanges();
            }

            base.Edit(employee);

        }

        public override IEnumerable<Employee> GetAll()
        {
            return _context.Employees
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Gender);
        }

        public IEnumerable<DTOEmployee> GetAllDto(string search = null)
        {
            var employees = _context.Employees.Include(x => x.Gender)
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country);

            if (!string.IsNullOrEmpty(search))
                employees = _context.Employees
                    .Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || x.Username.Contains(search)
                    || _context.EmployeeInventories.Where(y => y.EmployeeId == x.Id && y.EndDate == null).FirstOrDefault().Inventory.Name.Contains(search))
                    .Include(x => x.Gender)
                    .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country);

            return _mapper.Map<IEnumerable<DTOEmployee>>(employees);
        }

        public DTOEmployee GetByIdDto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gender> GetGenders()
        {
            return _context.Genders.ToList();
        }

        public double GetSalary(int employeeId)
        {
            return _context.EmployeeSalaries
                .Where(x => x.EmployeeId == employeeId && x.EndDate == null)
                .FirstOrDefault().Value;
        }

        public Inventory GetInventory(int employeeId)
        {
            EmployeeInventory employeeInventory = _context.EmployeeInventories
                .Where(x => x.EmployeeId == employeeId && x.EndDate == null)
                .Include(x => x.Inventory)
                .FirstOrDefault();
            if (employeeInventory == null)
                return null;
            else
                return employeeInventory.Inventory;

        }

        public override Employee GetById(int id)
        {
            return _context.Employees.Where(x => x.Id == id)
                .Include(x => x.Gender)
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .FirstOrDefault();
        }

        public override bool Delete(Employee employee)
        {
            if (employee == null)
                return false;
            foreach(var x in _context.EmployeeInventories
                .Where(x => x.EmployeeId == employee.Id).ToList())
            {
                _context.Remove(x);
            }
            foreach (var x in _context.EmployeeSalaries
                .Where(x => x.EmployeeId == employee.Id).ToList())
            {
                _context.Remove(x);
            }
            _context.SaveChanges();

            Address address = _context.Addresses.Find(employee.AddressId);
            _context.Remove(address);

            ApplicationUser user = _context.Users
                .Where(x => x.UserName == employee.Username).FirstOrDefault();
            ApplicationUserRole userRole = _context.UserRoles
                .Where(x => x.UserId == user.Id).FirstOrDefault();
            Person person = _context.Persons
                .Where(x => x.IdentityUserId == user.Id).FirstOrDefault();
            if (user != null)
            {
                _context.Remove(userRole);
                _context.Remove(person);
                _context.Remove(user);
            }

            //_context.Remove(employee);
            //_context.SaveChanges();
            return base.Delete(employee);

        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItemsByInventoryId(int inventoryId)
        {
            return await _context.EmployeeInventories
                .Where(x => x.InventoryId == inventoryId)
                .Include(x => x.Employee)
                .Select(x => new DTODropdown
                {
                    Id = x.EmployeeId.ToString(),
                    Value = x.Employee.Username
                }).ToListAsync();
        }

    }
}
