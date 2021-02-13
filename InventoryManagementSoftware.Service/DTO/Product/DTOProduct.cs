using InventoryManagementSoftware.Service.DTO.CategoryBrand;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace InventoryManagementSoftware.Service.DTO.Product
{
    public class DTOProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DTOCategoryBrand CategoryBrand { get; set; }
        public DTOProductPrice ProductPrice { get; set; }
        public List<DTOProductShelf> ProductShelves { get; set; }
        public List<DTOProductAttribute> Attributes { get; set; }
        public List<ProductAttributeList> ProductAttributeList { get; set; }
        public List<SelectListItem> AttributesDropDown { get; set; }
        public List<ProductInventoryList> productInventoryList { get; set; }


        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public int BrandId { get; set; }
        public List<SelectListItem> Brands { get; set; }
        public int CategoryBrandId { get; set; }
    }

    public class ProductAttributeList
    {
        public int AttributeId { get; set; }
        public string Value { get; set; }
        public List<SelectListItem> Attributes { get; set; }

    }

    public class ProductInventoryList
    {
        public int InventoryId { get; set; }
        public int DepartmentId { get; set; }
        public int ShelfId { get; set; }
        public double Quantity { get; set; }
    }
}
