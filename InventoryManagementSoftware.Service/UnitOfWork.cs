using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InventoryManagementSoftware.Service
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly IMSContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork( IMSContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        #region Registrovanje servisa


        private ICountryService _countryRepository;
        public ICountryService Countries => _countryRepository ?? (_countryRepository = _serviceProvider.GetService<ICountryService>());

        private ICityService _cityService;
        public ICityService Cities => _cityService ?? (_cityService = _serviceProvider.GetService<ICityService>());

        private IAddressService _addressService;
        public IAddressService Addresses => _addressService ?? (_addressService = _serviceProvider.GetService<IAddressService>());

        private ICustomerService _customerService;
        public ICustomerService Customers => _customerService ?? (_customerService = _serviceProvider.GetService<ICustomerService>());

        private IDepartmentService _departmentService;
        public IDepartmentService Departments => _departmentService ?? (_departmentService = _serviceProvider.GetService<IDepartmentService>());

        private IInventoryService _inventoryService;
        public IInventoryService Inventories => _inventoryService ?? (_inventoryService = _serviceProvider.GetService<IInventoryService>());

        private IPhoneNumberService _phoneNumberService;
        public IPhoneNumberService PhoneNumbers => _phoneNumberService ?? (_phoneNumberService = _serviceProvider.GetService<IPhoneNumberService>());

        private IShelvesService _shelvesService;
        public IShelvesService Shelves => _shelvesService ?? (_shelvesService = _serviceProvider.GetService<IShelvesService>());

        private ISupplierService _supplierService;
        public ISupplierService Suppliers => _supplierService ?? (_supplierService = _serviceProvider.GetService<ISupplierService>());

        private IBrandService _brandService;
        public IBrandService Brands => _brandService ?? (_brandService = _serviceProvider.GetService<IBrandService>());

        private ICategoryService _categoryService;
        public ICategoryService Categories => _categoryService ?? (_categoryService = _serviceProvider.GetService<ICategoryService>());

        private ICategoryBrandService _categoryBrandService;
        public ICategoryBrandService CategoriesBrands => _categoryBrandService ?? (_categoryBrandService = _serviceProvider.GetService<ICategoryBrandService>());

        private IProductService _productService;
        public IProductService Products => _productService ?? (_productService = _serviceProvider.GetService<IProductService>());

        private IProductAttributeService _productAttributeService;
        public IProductAttributeService ProductAttributes => _productAttributeService ?? (_productAttributeService = _serviceProvider.GetService<IProductAttributeService>());

        private IProductShelfService _productShelfService;
        public IProductShelfService ProductShelves => _productShelfService ?? (_productShelfService = _serviceProvider.GetService<IProductShelfService>());

        private IProductPriceService _productPriceService;
        public IProductPriceService ProductPrices => _productPriceService ?? (_productPriceService = _serviceProvider.GetService<IProductPriceService>());


        private IAttributeService _attributeService;
        public IAttributeService Attributes => _attributeService ?? (_attributeService = _serviceProvider.GetService<IAttributeService>());

        private IUserService _userService;
        public IUserService Users => _userService ?? (_userService = _serviceProvider.GetService<IUserService>());

        private IEmployeeService employeeService;
        public IEmployeeService Employees => employeeService ?? (employeeService = _serviceProvider.GetService<IEmployeeService>());

        private IExportService exportService;
        public IExportService Exports => exportService ?? (exportService = _serviceProvider.GetService<IExportService>());

        private IImportService ImportService;
        public IImportService Imports => ImportService ?? (ImportService = _serviceProvider.GetService<IImportService>());


        #endregion

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _context.Database.CurrentTransaction?.Commit();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Rollback()
        {
            _context.Database.CurrentTransaction?.Rollback();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void UpdateRange(params object[] entities)
        {
            _context.UpdateRange(entities);
        }
    }
}
