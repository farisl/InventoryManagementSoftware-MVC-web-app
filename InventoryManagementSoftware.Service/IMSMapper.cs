using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.DTO.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace InventoryManagementSoftware.Service
{
    public class IMSMapper : Profile
    {
        public IMSMapper()
        {
            CreateMap<DTO.Address.DTOAddress, Address>()
                .ForMember(x => x.City, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.City.DTOCity, City>()
                .ForMember(x => x.Country, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Department.DTODepartment, Department>()
                .ForMember(x => x.Inventory, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Inventory.DTOInventory, Inventory>()
                .ForMember(x => x.Address, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Shelves.DTOShelves, Shelves>()
                .ForMember(x => x.Department, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Export.DTOExport, Export>()
                .ForMember(x => x.Inventory, opt => opt.Ignore())
                .ForMember(x => x.Customer, opt => opt.Ignore())
                .ForMember(x => x.Employee, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Import.DTOImport, Import>()
                .ForMember(x => x.Inventory, opt => opt.Ignore())
                .ForMember(x => x.Supplier, opt => opt.Ignore())
                .ForMember(x => x.Employee, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Supplier.DTOSupplier, Supplier>()
                .ForMember(x => x.Address, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Customer.DTOCustomer, Customer>()
                .ForMember(x => x.Address, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Brand.DTOBrand, Brand>()
                .ReverseMap()
                .ForMember(x => x.CategoryBrandsListItems, opt => opt.Ignore());

            CreateMap<DTO.Category.DTOCategory, Category>()
                .ReverseMap()
                .ForMember(x => x.CategoryBrandsListItems, opt => opt.Ignore());

            CreateMap<DTO.CategoryBrand.DTOCategoryBrand, CategoryBrand>()
                .ReverseMap();

            CreateMap<DTO.Product.DTOProduct, Product>()
                .ForMember(x => x.CategoryBrand, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Product.DTOProductAttribute, ProductAttribute>()
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Attribute, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Export.DTOExportDetail, ExportDetail>()
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Export, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Export.ExportProductList, ExportDetail>()
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Import.DTOImpotDetail, ImportDetail>()
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Import, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Import.ImportProductList, ImportDetail>()
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Product.DTOProductShelf, ProductShelf>()
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ForMember(x => x.Shelf, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Product.ProductAttributeList, ProductAttribute>()
                .ReverseMap()
                .ForMember(x => x.Attributes, opt => opt.Ignore());

            CreateMap<DTO.Employee.DTOEmployee, Employee>()
                .ForMember(x => x.Address, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DTO.Attribute.DTOAttribute, Attribute>()
                .ReverseMap();
            CreateMap<DTO.Product.DTOProductPrice, ProductPrice>()
                .ReverseMap();

            CreateMap<DTODropdown, SelectListItem>()
                .ForMember(x => x.Text, opt => opt.MapFrom(y => y.Value))
                .ForMember(x => x.Value, opt => opt.MapFrom(y => y.Id));

            CreateMap<ProductShelf, DTO.Product.ProductInventoryList>()
                .ReverseMap()
                .ForMember(x => x.ProductId, opt => opt.Ignore())
                .ForMember(x => x.Shelf, opt => opt.Ignore());

            CreateMap<ApplicationUser, DTOUser>()
                .ForMember(x=>x.Role, opt=>opt.MapFrom(x=>x.UserRoles.FirstOrDefault().Role))
                .ForMember(x=>x.Area, opt=>opt.MapFrom(x=>x.UserRoles.FirstOrDefault().Role))
                .ReverseMap();
        }
    
    } 
}


        