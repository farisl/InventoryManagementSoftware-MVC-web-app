using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace InventoryManagementSoftware.Service
{
    public class IMSContext : IdentityDbContext<ApplicationUser,ApplicationRole, int,IdentityUserClaim<int>,ApplicationUserRole,IdentityUserLogin<int>,IdentityRoleClaim<int>,IdentityUserToken<int>>
    { 
        public IMSContext(DbContextOptions<IMSContext> options) : base(options)
        {}
        
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Shelves> Shelves { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryBrand> CategoriesBrands { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductShelf> ProductShelves { get; set; }
        public virtual DbSet<ProductPrice> ProductPrices { get; set; }
        public virtual DbSet<Core.Models.Attribute> Attributes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationApplicationUser> UserNotifications { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeInventory> EmployeeInventories { get; set; }
        public virtual DbSet<EmployeeSalaries> EmployeeSalaries { get; set; }
        public virtual DbSet<Export> Exports { get; set; }
        public virtual DbSet<ExportDetail> ExportDetails { get; set; }
        public virtual DbSet<Import> Imports { get; set; }
        public virtual DbSet<ImportDetail> ImportDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>().HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            builder.Entity<ApplicationUserRole>().HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);

            builder.Entity<ApplicationRole>().HasData(Enum.GetValues(typeof(Roles.Role)).Cast<Roles.Role>().Select(x => new ApplicationRole
            {
                Id = (int)x,
                Name = x.ToString(),
                NormalizedName = x.ToString().ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }));

            builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole()
            {
                RoleId = 1,
                UserId = 1
            }, new ApplicationUserRole 
            {
                RoleId = 1,
                UserId = 2
            });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = 1,
                Email = "farisl@ims.ba",
                NormalizedEmail = "farisl@ims.ba".ToUpper(),
                UserName = "farisl",
                NormalizedUserName = "farisl".ToUpper(),
                EmailConfirmed = true,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = "AQAAAAEAACcQAAAAEA27GXFgUsI5e3+EHt0MSqROepea6LqlbFSugDckdIrVK+MyaYbiqZABm4qUmjep+A==",    // Demo12345*             
            }, new ApplicationUser 
            {
                Id = 2,
                Email = "omarl@ims.ba",
                NormalizedEmail = "omarl@ims.ba".ToUpper(),
                UserName = "omarl",
                NormalizedUserName = "omarl".ToUpper(),
                EmailConfirmed = true,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = "AQAAAAEAACcQAAAAEA27GXFgUsI5e3+EHt0MSqROepea6LqlbFSugDckdIrVK+MyaYbiqZABm4qUmjep+A=="
            });

            builder.Entity<Gender>().HasData(new Gender
            {
                Id = 1,
                Name = "Male"
            }, new Gender
            {
                Id = 2,
                Name = "Female"
            });

            builder.Entity<Person>().HasData(new Person
            {
                Id = 1,
                Active = true,
                FirstName = "Faris",
                LastName = "Lekić",
                CreatedDate = DateTime.Now,
                GenderId = 1,
                IdentityUserId = 1
            }, new Person 
            {
                Id = 2,
                Active = true,
                FirstName = "Omar",
                LastName = "Lapo",
                CreatedDate = DateTime.Now,
                GenderId = 1,
                IdentityUserId = 2
            });

        }

    }
}
