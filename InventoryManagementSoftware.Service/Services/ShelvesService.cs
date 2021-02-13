using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.DTO.Shelves;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.Services
{
    public class ShelvesService : BaseService<Shelves> , IShelvesService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;
        public ShelvesService(IMSContext context, IMapper mapper): base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public override IEnumerable<Shelves> GetAll()
        {
            return _context.Shelves
                   .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                   .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.PhoneNumber);
        }
        public override Shelves GetById(int id)
        {
            return _context.Shelves
                 .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                 .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.PhoneNumber)
                 .Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Shelves> GetByDepartmentId(int id)
        {
            return _context.Shelves
                .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.PhoneNumber)
                .Where(x => x.DepartmentId == id);
        }

        public IEnumerable<DTOShelves> GetByDepartmentIdDto(int id)
        {
            var shelves = _context.Shelves
                .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.PhoneNumber)
                .Where(x => x.DepartmentId == id);
            return _mapper.Map<IEnumerable<DTOShelves>>(shelves);
        }

        public IEnumerable<DTOShelves> GetAllDto()
        {
            var shelves = _context.Shelves
                    .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                    .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.PhoneNumber);
            return _mapper.Map<IEnumerable<DTOShelves>>(shelves);
        }

        public DTOShelves GetByIdDto(int id)
        {
            var shelve = _context.Shelves
                .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.Department).ThenInclude(x => x.Inventory).ThenInclude(x => x.PhoneNumber)
                .Where(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<DTOShelves>(shelve);
        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItems()
        {
            return await _context.Shelves.Select(x => new DTODropdown
            {
                Id = x.Id.ToString(),
                Value = x.Name                
            }).ToListAsync();           
        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItemsByDepartmentId(int departmentId)
        {
            return await _context.Shelves.Where(x=>x.DepartmentId == departmentId).Select(x => new DTODropdown
            {
                Id = x.Id.ToString(),
                Value = x.Name
            }).ToListAsync();
        }
    }
}
