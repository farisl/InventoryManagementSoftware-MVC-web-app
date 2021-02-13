using InventoryManagementSoftware.Service.IServices;
using InventoryManagementSoftware.Service.Services;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementSoftware.Service
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IPhoneNumberService, PhoneNumberService>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IShelvesService, ShelvesService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ISupplierService,SupplierService>();
            services.AddTransient<IBrandService,BrandService>();
            services.AddTransient<ICategoryService,CategoryService>();
            services.AddTransient<ICategoryBrandService,CategoryBrandService>();
            services.AddTransient<IProductService,ProductService>();
            services.AddTransient<IProductAttributeService,ProductAttributeService>();
            services.AddTransient<IProductShelfService,ProductShelfService>();
            services.AddTransient<IProductPriceService,ProductPriceService>();
            services.AddTransient<IAttributeService,AttributeService>();
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<INotificationService,NotificationService>();
            services.AddTransient<IEmployeeService,EmployeeService>();
            services.AddTransient<IExportService,ExportService>();
            services.AddTransient<IImportService, ImportService>();

            return services;
        }
    }
}
