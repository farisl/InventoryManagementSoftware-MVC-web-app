using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.CategoryBrand;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.DTO.Product;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSoftware.Service.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;
        public ProductService(IMSContext context,IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(DTOProduct model)
        {
            try
            {
                Product product = _mapper.Map<Product>(model);
                if (model.BrandId > 0 && model.CategoryId > 0)
                    product.CategoryBrandId = _context.CategoriesBrands.Where(x => x.BrandId == model.BrandId && x.CategoryId == model.CategoryId).Select(x => x.Id).FirstOrDefault();
                
                ProductPrice productPrice = _mapper.Map<ProductPrice>(model.ProductPrice);
                List<ProductAttribute> productAttributes = _mapper.Map<List<ProductAttribute>>(model.ProductAttributeList);
                List<ProductShelf> productShelves = _mapper.Map<List<ProductShelf>>(model.productInventoryList);


                _context.Products.Add(product);
                _context.SaveChanges();
                productPrice.ProductId = product.Id;

                if (productPrice != null)
                    _context.ProductPrices.Add(productPrice);

                if (productAttributes != null)
                {
                    foreach (var attr in productAttributes)
                        attr.ProductId = product.Id;

                    _context.ProductAttributes.AddRange(productAttributes);
                }

                if(productShelves != null)
                {
                    foreach (var prodShelf in productShelves)
                        prodShelf.ProductId = product.Id;

                    _context.ProductShelves.AddRange(productShelves);
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Edit(DTOProduct model)
        {
            try
            {

                if (model.Id == 0)
                    return false;

                Product product = _context.Products.Find(model.Id);

                if (product == null)
                    return false;


                product.Name = model.Name;
                product.Description = model.Description;
                if (model.CategoryBrand.BrandId > 0 && model.CategoryBrand.CategoryId > 0)
                    product.CategoryBrandId = _context.CategoriesBrands.Where(x => x.BrandId == model.CategoryBrand.BrandId && x.CategoryId == model.CategoryBrand.CategoryId).Select(x => x.Id).FirstOrDefault();


                ProductPrice productPrice = _context.ProductPrices.FirstOrDefault(x => x.ProductId == product.Id && x.IsDeleted == false);

                if (productPrice.Price != model.ProductPrice.Price) // Ukoliko se cijena ne slaze sa postojecom cijenom to znaci da je doslo do promjene cijene. Staru cijenu postavljamo na IsDeleted a dodajemo novu
                {
                    productPrice.IsDeleted = true;
                    ProductPrice newProductPrice = _mapper.Map<ProductPrice>(model.ProductPrice);

                    newProductPrice.ProductId = product.Id;
                    if (newProductPrice != null)
                    {
                        _context.ProductPrices.Add(newProductPrice);
                        productPrice.IsDeleted = true;
                    }

                }

                List<ProductAttribute> oldProductAttributes = _context.ProductAttributes.Where(x => x.ProductId == product.Id).ToList();
                _context.RemoveRange(oldProductAttributes);

                List<ProductAttribute> newProductAttributes = _mapper.Map<List<ProductAttribute>>(model.ProductAttributeList);

                if (newProductAttributes != null)
                {
                    foreach (var attr in newProductAttributes)
                        attr.ProductId = product.Id;

                    _context.ProductAttributes.AddRange(newProductAttributes);
                }



                List<ProductShelf> oldProductShelves = _context.ProductShelves.Where(x => x.ProductId == product.Id).ToList();
                _context.RemoveRange(oldProductShelves);

                List<ProductShelf> newProductShelves = _mapper.Map<List<ProductShelf>>(model.productInventoryList);

                if (newProductShelves != null)
                {
                    foreach (var attr in newProductShelves)
                        attr.ProductId = product.Id;

                    _context.ProductShelves.AddRange(newProductShelves);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<DTOProduct> GetAllDto(string search = null)
        {
            List<Product> products = null;
            if (!string.IsNullOrEmpty(search))
                products = _context.Products.Include(x => x.CategoryBrand)
                    .Where(x => x.IsDeleted == false && (x.Name.Contains(search)
                    || x.CategoryBrand.Category.Name.Contains(search) || x.CategoryBrand.Brand.Name.Contains(search)))
                    .ToList();
            else
                products = _context.Products.Include(x => x.CategoryBrand).Where(x => x.IsDeleted == false).ToList();
            List<ProductAttribute> attributes = _context.ProductAttributes.Where(pa => products.Select(x => x.Id).Contains(pa.ProductId)).ToList();
            List<ProductShelf> productShelves = _context.ProductShelves.Where(ps => products.Select(x => x.Id).Contains(ps.ProductId)).ToList();
            List<ProductPrice> productPrices = _context.ProductPrices.Where(pp => products.Select(x => x.Id).Contains(pp.ProductId)).ToList();

            List<DTOProduct> dtoProducts = products.Select(x => new DTOProduct
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CategoryBrand = _mapper.Map<DTOCategoryBrand>(x.CategoryBrand),
                Attributes = _mapper.Map<List<DTOProductAttribute>>(attributes.Where(a => a.ProductId == x.Id).ToList()),
                ProductPrice = _mapper.Map<DTOProductPrice>(productPrices.Where(pp => pp.ProductId == x.Id).FirstOrDefault()),
                ProductShelves = _mapper.Map<List<DTOProductShelf>>(productShelves.Where(ps => ps.ProductId == x.Id).ToList())
            }).ToList();

            return dtoProducts;
        }

        public DTOProduct GetByIdDto(int id)
        {
            Product product = _context.Products.FirstOrDefault(x=>x.IsDeleted == false && x.Id == id);

            if (product == null)
                return null;

            List<ProductAttribute> productAttributes = _context.ProductAttributes.Include(x => x.Attribute).Where(x => x.ProductId == product.Id).ToList();
            List<ProductShelf> productShelves = _context.ProductShelves.Where(x => x.ProductId == product.Id)
                                                                       .Include(x=>x.Shelf)
                                                                       .ThenInclude(x=>x.Department)
                                                                       .ThenInclude(x=>x.Inventory)
                                                                       .ToList();
            ProductPrice productPrice = _context.ProductPrices.FirstOrDefault(x => x.ProductId == product.Id && x.IsDeleted == false);
            CategoryBrand categoryBrand = _context.CategoriesBrands.Include(x => x.Category)
                                                                   .Include(x => x.Brand)
                                                                   .FirstOrDefault(x => x.Id == product.CategoryBrandId);
            DTOProduct model = _mapper.Map<DTOProduct>(product);
            model.Attributes = _mapper.Map<List<DTOProductAttribute>>(productAttributes);
            model.ProductPrice = _mapper.Map<DTOProductPrice>(productPrice);
            model.ProductShelves = _mapper.Map<List<DTOProductShelf>>(productShelves);
            model.CategoryBrand = _mapper.Map<DTOCategoryBrand>(categoryBrand);

            return model;
        }

        public DTOProduct GetDetailsByIdDto(int productId)
        {
            Product product = _context.Products.FirstOrDefault(x=>x.Id == productId && x.IsDeleted == false);

            if (product == null)
                return null;
            List<ProductAttribute> productAttributes = _context.ProductAttributes.Where(x => x.ProductId == product.Id).Include(x=>x.Attribute).ToList();
            List<ProductShelf> productShelves = _context.ProductShelves.Where(x => x.ProductId == product.Id).ToList();
            ProductPrice productPrice = _context.ProductPrices.FirstOrDefault(x => x.ProductId == product.Id && x.IsDeleted == false);

            DTOProduct model = _mapper.Map<DTOProduct>(product);
            model.Attributes = _mapper.Map<List<DTOProductAttribute>>(productAttributes);
            model.ProductPrice = _mapper.Map<DTOProductPrice>(productPrice);
            model.ProductShelves = _mapper.Map<List<DTOProductShelf>>(productShelves);

            return model;
        }

        public bool SoftDelete(int productId)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
                return false;

            product.IsDeleted = true;

            _context.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItems()
        {
            return await _context.Products.Select(x => new DTODropdown
            {
                Id = x.Id.ToString(),
                Value = x.Name
            }).ToListAsync();
        }

    }
}
