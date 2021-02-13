using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Brand;
using InventoryManagementSoftware.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

using static InventoryManagementSoftware.Web.Extensions;


namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{

    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Resource _localizer;
        public BrandController(IUnitOfWork unitOfWork, Resource localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            List<DTOBrand> model = _unitOfWork.Brands.GetAllDto().ToList();
            return View(model);
        }

        public IActionResult Add()
        {
            DTOBrand model = new DTOBrand();
            model.CategoryBrandsListItems = _unitOfWork.Categories.GetAll().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return PartialView("AddEdit", model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            DTOBrand model = _unitOfWork.Brands.GetByIdDto(id.GetValueOrDefault());
            return PartialView("AddEdit", model);
        }

        public IActionResult Save(DTOBrand model)
        {
            if (!IsSet(model.Name))
                ModelState.AddModelError(nameof(model.Name), _localizer.ErrNameIsRequired);

            if (!ModelState.IsValid)
            {
                model.CategoryBrandsListItems = _unitOfWork.Brands.GetAll().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                return PartialView("AddEdit", model);
            }

            if (model.Id == 0)
            {
                _unitOfWork.Brands.Add(model);
            }
            else
            {
                _unitOfWork.Brands.Edit(model);
            }

            return Json(new
            {
                success = true
            });
        }
        public IActionResult Delete(int id)
        {
            if (id != 0)
                _unitOfWork.Brands.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}