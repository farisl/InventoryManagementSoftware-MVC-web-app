using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Customer;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Service.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;
        public CustomerService(IMSContext context, IMapper mapper): base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(DTOCustomer model)
        {
            Address address= _mapper.Map<Address>(model.Address);
            PhoneNumber phone = model.PhoneNumber;
            EmailAddress email= model.EmailAddress;
            Customer customer = _mapper.Map<Customer>(model);

            _context.Add(address);
            _context.Add(phone);
            _context.Add(email);
            _context.SaveChanges();

            customer.AddressId = address.Id;
            customer.PhoneNumberId = phone.Id;
            customer.EmailAddressId = email.Id;

            base.Add(customer);
        }

        public void Edit(DTOCustomer model)
        {
            Address address = _mapper.Map<Address>(model.Address);
            PhoneNumber phone = model.PhoneNumber;
            EmailAddress email = model.EmailAddress; 
            Customer customer = _mapper.Map<Customer>(model);

            _context.Update(address);
            _context.Update(phone);
            _context.Update(email);
            _context.SaveChanges();

            base.Edit(customer);
        }
        public override IEnumerable<Customer> GetAll()
        {
            return _context.Customers
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber);
 
        }

        public IEnumerable<DTOCustomer> GetAllDto(string search = null)
        {
            var customer = _context.Customers
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber);

            if (!string.IsNullOrEmpty(search))
                customer = _context.Customers
                .Where(x => x.Name.Contains(search))
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber);

            return _mapper.Map<IEnumerable<DTOCustomer>>(customer);
        }
        public override Customer GetById(int id)
        {
            return _context.Customers
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber).Where(x=>x.Id==id).FirstOrDefault();
        }


        public IEnumerable<City> GetCities()
        {
            return _context.Cities.Include(x => x.Country);
        }

        public override bool Delete(Customer customer)
        {
            if (customer == null)
                return false;   

            Address address = _context.Addresses.Find(customer.AddressId);
            PhoneNumber number = _context.PhoneNumbers.Find(customer.PhoneNumberId);
            EmailAddress email = _context.EmailAddresses.Find(customer.EmailAddressId);
            _context.Addresses.Remove(address);
            _context.PhoneNumbers.Remove(number);
            _context.EmailAddresses.Remove(email);
            return base.Delete(customer);
           
        }

        public override bool Delete(int id)
        {
            if (id == 0)
                return false;
            Customer customer = _context.Customers.Find(id);
            Address address = _context.Addresses.Find(customer.AddressId);
            PhoneNumber number = _context.PhoneNumbers.Find(customer.PhoneNumberId);
            EmailAddress email = _context.EmailAddresses.Find(customer.EmailAddressId);
            _context.Addresses.Remove(address);
            _context.PhoneNumbers.Remove(number);
            _context.EmailAddresses.Remove(email);
            return base.Delete(id);
        }

        public DTOCustomer GetByIdDto(int id)
        {
            var customer = _context.Customers
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country)
                .Include(x => x.EmailAddress)
                .Include(x => x.PhoneNumber).Where(x => x.Id == id).FirstOrDefault();

            return _mapper.Map<DTOCustomer>(customer);
        }
    }
}
