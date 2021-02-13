using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Department;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;
        private readonly IShelvesService _shelvesService;
        public DepartmentService(IMSContext context, IMapper mapper, IShelvesService shelves): base(context)
        {
            _context = context;
            _mapper = mapper;
            _shelvesService = shelves;
        }

        public override IEnumerable<Department> GetAll()
        {
            return _context.Departments
                .Include(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Inventory).ThenInclude(x => x.PhoneNumber);                
        }
        public override Department GetById(int id)
        {
            return _context.Departments
                .Include(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Inventory).ThenInclude(x => x.PhoneNumber)
                .Where(d => d.Id == id).FirstOrDefault();
        }

        public IEnumerable<Department> GetByInventoryId(int id)
        {
            return _context.Departments
                .Include(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Inventory).ThenInclude(x => x.PhoneNumber)
                .Where(d => d.InventoryId == id);
                
        }


        public IEnumerable<DTODepartment> GetAllDto()
        {
            var departments = _context.Departments
                .Include(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Inventory).ThenInclude(x => x.PhoneNumber);
            return _mapper.Map<IEnumerable<DTODepartment>>(departments);
        }

        public DTODepartment GetByIdDto(int id)
        {
            var department = _context.Departments
                .Include(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Inventory).ThenInclude(x => x.PhoneNumber)
                .Where(d => d.Id == id).FirstOrDefault();

            return _mapper.Map<DTODepartment>(department);
        }

        public IEnumerable<DTODepartment> GetByInventoryIdDto(int id)
        {
            var departments = _context.Departments
                .Include(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Inventory).ThenInclude(x => x.PhoneNumber)
                .Where(d => d.InventoryId == id);

            return _mapper.Map<IEnumerable<DTODepartment>>(departments);
        }

        public override bool Delete(Department department)
        {
            if (department == null)
                return false;

            List<Shelves> shelves = _context.Shelves.Where(x => x.DepartmentId == department.Id).ToList();
            foreach (Shelves s in shelves)
                _shelvesService.Delete(s);

            return base.Delete(department);            
        }
        public override bool Delete(int id)
        {
            if (id == 0)
                return false;
            List<Shelves> shelves = _context.Shelves.Where(x => x.DepartmentId == id).ToList();
            foreach (Shelves s in shelves)
                _shelvesService.Delete(s);
            
            return base.Delete(id);
        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItems()
        {
            return await _context.Departments.Select(x => new DTODropdown
            {
                Id = x.Id.ToString(),
                Value = x.Name
            }).ToListAsync();
        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItemsByInventoryId(int inventoryId)
        {
            return await _context.Departments.Where(x=>x.InventoryId == inventoryId).Select(x => new DTODropdown
            {
                Id = x.Id.ToString(),
                Value = x.Name
            }).ToListAsync();
        }
    }
}
