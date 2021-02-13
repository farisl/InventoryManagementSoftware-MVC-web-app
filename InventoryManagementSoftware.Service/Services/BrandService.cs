using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Brand;
using InventoryManagementSoftware.Service.DTO.CategoryBrand;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSoftware.Service.Services
{
    public class BrandService : BaseService<Brand>, IBrandService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IMSContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void Add(DTOBrand dtoBrand)
        {
            Brand brand = _mapper.Map<Brand>(dtoBrand);

            _context.Add(brand);
            _context.SaveChanges();
            if (dtoBrand.SelectedCategoryBrandsIds != null && dtoBrand.SelectedCategoryBrandsIds.Any())
            {
                CategoryBrand categoryBrand = null;
                foreach (var cb in dtoBrand.SelectedCategoryBrandsIds)
                {
                    categoryBrand = new CategoryBrand
                    {
                        BrandId = brand.Id,
                        CategoryId = cb,
                    };

                    _context.CategoriesBrands.Add(categoryBrand);
                }
            }

            _context.SaveChanges();
        }

        public void Edit(DTOBrand dtoBrand)
        {
            Brand brand = _mapper.Map<Brand>(dtoBrand);
            _context.Update(brand);

            List<int> itemsToAdd = dtoBrand.SelectedCategoryBrandsIds;
            List<int> itemsToDelete = new List<int>();
            List<CategoryBrand> categoryBrands = _context.CategoriesBrands.Where(x => x.BrandId == brand.Id).ToList();

            foreach (var item in categoryBrands)
            {
                bool exist = itemsToAdd.Any(x => x == item.CategoryId);
                if (!exist)
                    itemsToDelete.Add(item.Id);
                else
                    itemsToAdd.Remove(item.CategoryId);
            }

            if (itemsToDelete != null && itemsToDelete.Any())
            {
                foreach (var item in itemsToDelete)
                    _unitOfWork.CategoriesBrands.Delete(item); //TODO : Ubaciti provjeru da li je povezan sa nekim postojecim PROIZVODOM
            }



            if (itemsToAdd != null && itemsToAdd.Any())
            {
                CategoryBrand categoryBrand = null;

                foreach (var item in itemsToAdd)
                {
                    categoryBrand = new CategoryBrand
                    {
                        BrandId = brand.Id,
                        CategoryId = item
                    };
                    _context.CategoriesBrands.Add(categoryBrand);
                }

            }
            _context.SaveChanges();

        }

        public IEnumerable<DTOBrand> GetAllDto()
        {
            var result = _context.Brands;
            return _mapper.Map<IEnumerable<DTOBrand>>(result);
        }
               
        public DTOBrand GetByIdDto(int id)
        {
            var categoryBrands = _context.CategoriesBrands.Where(x => x.BrandId == id).ToList();
            var brands = _context.Brands.Find(id);

            var categories = _context.Categories;
            var result = _mapper.Map<DTOBrand>(brands);
            result.SelectedCategoryBrandsIds = categoryBrands.Select(x => x.CategoryId).ToList();
            result.CategoryBrandsListItems = categories.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            result.CategoryBrands = _mapper.Map<List<DTOCategoryBrand>>(categoryBrands);
            return result;
        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItems()
        {
            return await _context.Brands.Select(x => new DTODropdown
            {
                Id = x.Id.ToString(),
                Value = x.Name
            }).ToListAsync();
        }
    }
}
