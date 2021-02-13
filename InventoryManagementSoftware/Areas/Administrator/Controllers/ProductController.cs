using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Product;
using InventoryManagementSoftware.Service.IServices;
using InventoryManagementSoftware.Web.Helpers.Dropdown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static InventoryManagementSoftware.Web.Extensions;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{

    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Resource _localizer;
        private readonly IDropdown _dropdown;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notification;
        private readonly IMSContext _context;
        public ProductController(IUnitOfWork unitOfWork, Resource localizer, IDropdown dropdown,
            UserManager<ApplicationUser> userManager, INotificationService notification, IMSContext context)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _dropdown = dropdown;
            _userManager = userManager;
            _notification = notification;
            _context = context;
        }
        public IActionResult Index(string search = null)
        {
            List<DTOProduct> model = _unitOfWork.Products.GetAllDto(search).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            DTOProduct model = new DTOProduct();
            
            model.ProductPrice = new DTOProductPrice();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(DTOProduct model)
        {

            if (!IsSet(model.Name))
                ModelState.AddModelError(nameof(model.Name), _localizer.ErrNameIsRequired);

            if (model.ProductPrice != null && model.ProductPrice.Price < 0)
                ModelState.AddModelError(nameof(model.ProductPrice.Price), "Cijena je obavezna");

            if (model.BrandId <= 0)
                ModelState.AddModelError(nameof(model.BrandId), "Brend je obavezan");

            if (model.CategoryId <= 0)
                ModelState.AddModelError(nameof(model.CategoryId), "Kategorija je obavezna");

            if (!ModelState.IsValid)
                return View("Add", model);

            try
            {
                _unitOfWork.Products.Add(model);

                var n = new Notification
                {
                    DateTime = DateTime.Now,
                    Text = $"New product - {model.Name}",
                    UserId = int.Parse(_userManager.GetUserId(HttpContext.User))
                };
                _notification.Create(n);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }


            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (Id == 0)
                return BadRequest();
            DTOProduct model = new DTOProduct();
            model = _unitOfWork.Products.GetByIdDto(Id);
            if (model != null)
                return View("Edit", model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(DTOProduct model)
        {
            if (!IsSet(model.Name))
                ModelState.AddModelError(nameof(model.Name), _localizer.ErrNameIsRequired);

            if (model.ProductPrice != null && model.ProductPrice.Price < 0)
                ModelState.AddModelError(nameof(model.ProductPrice.Price), "Cijena je obavezna");

            if (model.CategoryBrand.BrandId <= 0)
                ModelState.AddModelError(nameof(model.BrandId), "Brend je obavezan");

            if (model.CategoryBrand.CategoryId <= 0)
                ModelState.AddModelError(nameof(model.CategoryId), "Kategorija je obavezna");

            if (!ModelState.IsValid)
                return View("Edit", model);

            try
            {
                _unitOfWork.Products.Edit(model);
            }
            catch
            {

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int ProductId)
        {
            DTOProduct model = new DTOProduct();
            model = _unitOfWork.Products.GetDetailsByIdDto(ProductId);
            if (model != null)
                return PartialView("Details", model);

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int productId)
        {
            var productName = _context.Products.FirstOrDefault(x => x.Id == productId).Name;
            var n = new Notification
            {
                DateTime = DateTime.Now,
                Text = $"Removed product - {productName}",
                UserId = int.Parse(_userManager.GetUserId(HttpContext.User))
            };
            _notification.Create(n);


            _unitOfWork.Products.SoftDelete(productId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IEnumerable<SelectListItem>> GetDepartments(int inventoryId)
        {
            return await _dropdown.DepartmentsByInventoryId(inventoryId);
        }
        [HttpGet]
        public async Task<IEnumerable<SelectListItem>> GetShelves(int departmentId)
        {
            return await _dropdown.ShelvesByDepartmentId(departmentId);           
        }
        [HttpGet]
        public async Task<IEnumerable<SelectListItem>> GetCategories(int brandId)
        {
            return await _dropdown.CategoriesByBrandId(brandId);
        }

    }
}