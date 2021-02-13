using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Supplier;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notification;

        public SupplierController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, INotificationService notification)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _notification = notification;
        }
        public IActionResult Index(string search = null)
        {
            List<DTOSupplier> model = _unitOfWork.Suppliers.GetAllDto(search).ToList();           
            return View(model);
        }

        public IActionResult AddSupplier()
        {
            DTOSupplier model = new DTOSupplier();
            model.Cities = _unitOfWork.Suppliers.GetCities().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return View("AddEditSupplier", model);
        }

        public IActionResult EditSupplier(int id)
        {

            DTOSupplier model = _unitOfWork.Suppliers.GetByIdDto(id);
            if (model == null)
                return RedirectToAction(nameof(Index));

            model.Cities = _unitOfWork.Suppliers.GetCities().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return View("AddEditSupplier", model);
        }

        public IActionResult SaveSupplier(DTOSupplier model)
        {
            if (model.Id == 0)
            {
                _unitOfWork.Suppliers.Add(model);

                var n = new Notification
                {
                    DateTime = DateTime.Now,
                    Text = $"New supplier - {model.Name}",
                    UserId = int.Parse(_userManager.GetUserId(HttpContext.User))
                };
                _notification.Create(n);
            }
            else
            {
                _unitOfWork.Suppliers.Edit(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteSupplier(int id)
        {
            Supplier supplier = _unitOfWork.Suppliers.GetById(id);
            if (supplier != null)
            {
                var n = new Notification
                {
                    DateTime = DateTime.Now,
                    Text = $"Removed supplier - {supplier.Name}",
                    UserId = int.Parse(_userManager.GetUserId(HttpContext.User))
                };
                _notification.Create(n);

                _unitOfWork.Suppliers.Delete(supplier);
            }               

            return RedirectToAction(nameof(Index));
        }
    }


}