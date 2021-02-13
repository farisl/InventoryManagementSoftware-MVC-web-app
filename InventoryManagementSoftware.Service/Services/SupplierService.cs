using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Supplier;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{
    public class SupplierService: BaseService<Supplier>, ISupplierService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;
        public SupplierService(IMSContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Add(DTOSupplier model)
        {
            Address address = _mapper.Map<Address>(model.Address);
            PhoneNumber phone = model.PhoneNumber;
            EmailAddress email = model.EmailAddress;
            Supplier supplier = _mapper.Map<Supplier>(model);

            _context.Add(address);
            _context.Add(phone);
            _context.Add(email);
            _context.SaveChanges();

            supplier.AddressId = address.Id;
            supplier.PhoneNumberId = phone.Id;
            supplier.EmailAddressId = email.Id;

            _context.Add(supplier);
            _context.SaveChanges();
        }

        public void Edit(DTOSupplier model)
        {
            Address address = _mapper.Map<Address>(model.Address);
            PhoneNumber phone = model.PhoneNumber;
            EmailAddress email = model.EmailAddress;
            Supplier supplier = _mapper.Map<Supplier>(model);

            _context.Update(address);
            _context.Update(phone);
            _context.Update(email);
            _context.Update(supplier);
            _context.SaveChanges();
        }

        public override IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber);
        }
        public override Supplier GetById(int id)
        {
            return _context.Suppliers
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber).Where(x => x.Id == id).FirstOrDefault();
        }


        public IEnumerable<City> GetCities()
        {
            return _context.Cities.Include(x => x.Country);
        }

        public override bool Delete(Supplier supplier)
        {
            Address address = _context.Addresses.Find(supplier.AddressId);
            PhoneNumber number = _context.PhoneNumbers.Find(supplier.PhoneNumberId);
            EmailAddress email = _context.EmailAddresses.Find(supplier.EmailAddressId);
            _context.Addresses.Remove(address);
            _context.PhoneNumbers.Remove(number);
            _context.EmailAddresses.Remove(email);
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            _context.Dispose();
            return true;
        }

        public override bool Delete(int id)
        {
            Supplier supplier = _context.Suppliers.Find(id);
            Address address = _context.Addresses.Find(supplier.AddressId);
            PhoneNumber number = _context.PhoneNumbers.Find(supplier.PhoneNumberId);
            EmailAddress email = _context.EmailAddresses.Find(supplier.EmailAddressId);
            _context.Addresses.Remove(address);
            _context.PhoneNumbers.Remove(number);
            _context.EmailAddresses.Remove(email);
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            _context.Dispose();
            return true;
        }

        public IEnumerable<DTOSupplier> GetAllDto(string search = null)
        {
            var suppliers = _context.Suppliers
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber);

            if (!string.IsNullOrEmpty(search))
                suppliers = _context.Suppliers
                .Where(x => x.Name.Contains(search))
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber);

            return _mapper.Map<IEnumerable<DTOSupplier>>(suppliers);
        }

        public DTOSupplier GetByIdDto(int id)
        {
            var supplier = _context.Suppliers
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber).Where(x => x.Id == id).FirstOrDefault();

            return _mapper.Map<DTOSupplier>(supplier);
        }
    }
}
