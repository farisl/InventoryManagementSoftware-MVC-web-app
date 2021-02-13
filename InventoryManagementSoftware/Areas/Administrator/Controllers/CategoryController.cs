using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using static InventoryManagementSoftware.Web.Extensions;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Resource _localizer;
        public CategoryController(IUnitOfWork unitOfWork, Resource localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            List<DTOCategory> model = _unitOfWork.Categories.GetAllDto().ToList();
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            DTOCategory model = new DTOCategory();
            model.CategoryBrandsListItems = _unitOfWork.Brands.GetAll().Select(x=> new SelectListItem(x.Name,x.Id.ToString())).ToList();

            return PartialView("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                return RedirectToAction(nameof(Index));

            DTOCategory model = new DTOCategory();
            model = _unitOfWork.Categories.GetByIdDto(id);

            return PartialView("AddEdit", model);
        }

        [HttpPost]
        public IActionResult Save(DTOCategory model)
        {
            if (!IsSet(model.Name))
                ModelState.AddModelError(nameof(model.Name), _localizer.ErrNameIsRequired);

            if (model.SelectedCategoryBrandsIds == null || model.SelectedCategoryBrandsIds.Count == 0)
                ModelState.AddModelError(nameof(model.SelectedCategoryBrandsIds), _localizer.ErrBrandIsRequired);

            if (!ModelState.IsValid)
            {
                model.CategoryBrandsListItems = _unitOfWork.Brands.GetAll().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                return PartialView("AddEdit", model);
            }

            if (model.Id == 0)
            {
                _unitOfWork.Categories.Add(model);
            }
            else
            {
                _unitOfWork.Categories.Edit(model);
            }

            return Json(new
            {
                success = true,
            });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id != 0)
                _unitOfWork.Categories.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}