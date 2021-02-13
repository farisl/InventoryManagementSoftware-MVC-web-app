using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace InventoryManagementSoftware.Service
{
    public interface IUnitOfWork : IDisposable
    {               
        int SaveChanges();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void Rollback();
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRange(params object[] entities);

        #region Registrovanje servisa
        IAddressService Addresses { get; }
        IAttributeService Attributes { get; }
        IBrandService Brands { get; }
        ICategoryService Categories { get; }
        ICategoryBrandService CategoriesBrands { get; }
        ICountryService Countries { get; }
        ICityService Cities { get; }
        ICustomerService Customers { get; }
        IDepartmentService Departments { get; }
        IEmployeeService Employees { get; }
        IExportService Exports { get; }
        IImportService Imports { get; }

        IInventoryService Inventories { get; }
        IPhoneNumberService PhoneNumbers { get; }
        IProductService Products { get; }
        IProductAttributeService ProductAttributes { get; }
        IProductShelfService ProductShelves { get; }
        IProductPriceService ProductPrices { get; }
        IShelvesService Shelves { get; }
        ISupplierService Suppliers { get; }
        IUserService Users { get; }
        #endregion

    }
}
