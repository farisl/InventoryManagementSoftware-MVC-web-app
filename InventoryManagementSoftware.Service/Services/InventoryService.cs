using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.DTO.Inventory;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.Services
{
    public class InventoryService : BaseService<Inventory>, IInventoryService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;
        private readonly IDepartmentService _departmentService;
        public InventoryService(IMSContext context,IMapper mapper, IAddressService address, IDepartmentService department) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _addressService = address;
            _departmentService = department;
        }
        void IInventoryService.Add(DTOInventory inventory)
        {
            PhoneNumber phone = inventory.PhoneNumber;
            Address address = _mapper.Map<Address>(inventory.Address);
            Inventory i = _mapper.Map<Inventory>(inventory);

            _context.Add(phone);
            _context.Add(address);
            _context.SaveChanges();

            i.PhoneNumberId = phone.Id;
            i.AddressId = address.Id;
            _context.Add(i);
            _context.SaveChanges();
        }

        void IInventoryService.Edit(DTOInventory inventory)
        {
            Address address = _mapper.Map<Address>(inventory.Address);
            _context.Update(address);
            PhoneNumber phone = _mapper.Map<PhoneNumber>(inventory.PhoneNumber);
            _context.Update(phone);
            base.Edit(_mapper.Map<Inventory>(inventory));
        }

        public override IEnumerable<Inventory> GetAll()
        {
            return _context.Inventories.Include(x => x.Address)
                .ThenInclude(x=>x.City)
                .ThenInclude(x=>x.Country)
                .Include(x=>x.PhoneNumber);
        }

        public override Inventory GetById(int id)
        {
            return _context.Inventories.Include(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.PhoneNumber)
                .Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<DTOInventory> GetAllDto()
        {
            var inventories = _context.Inventories.Include(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.PhoneNumber);
            return _mapper.Map<IEnumerable<DTOInventory>>(inventories);
        }

        public DTOInventory GetByIdDto(int id)
        {
            var inventory= _context.Inventories.Include(x => x.Address)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .Include(x => x.PhoneNumber)
                .Where(x => x.Id == id).FirstOrDefault();
            return _mapper.Map<DTOInventory>(inventory);
        }

        public override bool Delete(Inventory inventory)
        {
            if (inventory == null)
                return false;
            List<Department> departments = _context.Departments.Where(x => x.InventoryId == inventory.Id).ToList();
            foreach (Department d in departments)
                _departmentService.Delete(d);

           return base.Delete(inventory);           
        }

        public override bool Delete(int id)
        {
            if (id==0)
                return false;
            List<Department> departments = _context.Departments.Where(x => x.InventoryId == id).ToList();
            foreach (Department d in departments)
                _departmentService.Delete(d);

            return base.Delete(id);
        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItems()
        {
            return await _context.Inventories.Select(x => new DTODropdown
            {
                Id = x.Id.ToString(),
                Value = x.Name
            }).ToListAsync(); 
        }
    }
}
