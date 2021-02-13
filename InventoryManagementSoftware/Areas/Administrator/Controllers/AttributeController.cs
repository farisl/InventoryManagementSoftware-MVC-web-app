using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static InventoryManagementSoftware.Web.Extensions;


namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{

    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class AttributeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Resource _localizer;
        public AttributeController(IUnitOfWork unitOfWork, Resource localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            List<DTOAttribute> model = new List<DTOAttribute>();
            model = _unitOfWork.Attributes.GetAllDto();
            return View(model);
        }


        public IActionResult Add()
        {
            DTOAttribute model = new DTOAttribute();
            return PartialView("AddEdit", model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            DTOAttribute model = _unitOfWork.Attributes.GetByIdDto(id.GetValueOrDefault());
            return PartialView("AddEdit", model);
        }

        public IActionResult Save(DTOAttribute model)
        {
            if (!IsSet(model.Name))
                ModelState.AddModelError(nameof(model.Name), _localizer.ErrNameIsRequired);

            if (!ModelState.IsValid)
            {
                return PartialView("AddEdit", model);
            }
            try
            {
                if (model.Id == 0)
                {
                    _unitOfWork.Attributes.Add(model);
                }
                else
                {
                    _unitOfWork.Attributes.Edit(model);
                }

                return Json(new
                {
                    success = true
                });

            }
            catch
            {
                return Ok();
            }
        }

        public IActionResult Delete(int id)
        {
            if (id != 0)
                _unitOfWork.Attributes.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}