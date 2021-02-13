using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Category;
using InventoryManagementSoftware.Service.DTO.CategoryBrand;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.Services
{
    class CategoryService : BaseService<Category> , ICategoryService
    {
        private readonly IMSContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IMSContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }               
        public void Add(DTOCategory dtoCategory)
        {
            Category category = _mapper.Map<Category>(dtoCategory);

            _context.Add(category);
            _context.SaveChanges();

            CategoryBrand categoryBrand = null;
            foreach (var cb in dtoCategory.SelectedCategoryBrandsIds)
            {
                categoryBrand = new CategoryBrand
                {
                    CategoryId = category.Id,
                    BrandId = cb
                };

                _context.CategoriesBrands.Add(categoryBrand);
            }

            _context.SaveChanges();
        }

        public void Edit(DTOCategory dtoCategory)
        {
            Category category = _mapper.Map<Category>(dtoCategory);
            _context.Update(category);

            List<int> itemsToAdd = dtoCategory.SelectedCategoryBrandsIds;
            List<int> itemsToDelete = new List<int>();
            List<CategoryBrand> categoryBrands = _context.CategoriesBrands.Where(x => x.CategoryId == category.Id).ToList();
            
            foreach (var item in categoryBrands)
            {
                bool exist = itemsToAdd.Any(x => x == item.BrandId);
                if (!exist)
                    itemsToDelete.Add(item.Id);
                else
                    itemsToAdd.Remove(item.BrandId);
            }

            if(itemsToDelete != null && itemsToDelete.Any())
            {
                foreach (var item in itemsToDelete)
                    _unitOfWork.CategoriesBrands.Delete(item); //TODO : Ubaciti provjeru da li je povezan sa nekim postojecim PROIZVODOM
            }



            if (itemsToAdd != null && itemsToAdd.Any())
            {
                CategoryBrand categoryBrand = null;
                
                foreach(var item in itemsToAdd)
                {
                    categoryBrand = new CategoryBrand
                    {
                        CategoryId = dtoCategory.Id,
                        BrandId = item
                    };
                    _context.CategoriesBrands.Add(categoryBrand);
                }

            }
            _context.SaveChanges();

        }               
        public IEnumerable<DTOCategory> GetAllDto()
        {
            var result = _context.Categories;
            return _mapper.Map<IEnumerable<DTOCategory>>(result);
        }

        public DTOCategory GetByIdDto(int id)
        {
            var categoryBrands = _context.CategoriesBrands.Where(x => x.CategoryId == id).ToList();
            var category = _context.Categories.Find(id);

            var brands = _context.Brands;
            var result = _mapper.Map<DTOCategory>(category);
            result.SelectedCategoryBrandsIds = categoryBrands.Select(x=>x.BrandId).ToList();
            result.CategoryBrandsListItems = brands.Select(x=> new SelectListItem(x.Name,x.Id.ToString())).ToList();
            result.CategoryBrands = _mapper.Map<List<DTOCategoryBrand>>(categoryBrands);
            return result;
        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItemsByBrandId(int brandId)
        {
            return await _context.CategoriesBrands
                                 .Include(x => x.Category)
                                 .Where(x => x.BrandId == brandId)
                                 .Select(x => new DTODropdown
                                 {
                                     Id = x.CategoryId.ToString(),
                                     Value = x.Category.Name
                                 }).ToListAsync();
        }
    }
}
