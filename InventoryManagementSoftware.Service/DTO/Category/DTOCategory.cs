using InventoryManagementSoftware.Service.DTO.CategoryBrand;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSoftware.Service.DTO.Category
{
    public class DTOCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem>  CategoryBrandsListItems { get; set; }
        public List<int> SelectedCategoryBrandsIds { get; set; }
        public List<DTOCategoryBrand> CategoryBrands { get; set; }

    }
}
