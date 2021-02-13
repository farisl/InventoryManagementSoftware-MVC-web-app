using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Supplier;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Areas.User.Controllers
{
    [Area("User")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notification;

        public SupplierController(ISupplierService supplier, UserManager<ApplicationUser> userManager, INotificationService notification)
        {
            _supplierService = supplier;
            _userManager = userManager;
            _notification = notification;
        }
        public IActionResult Index()
        {
            List<DTOSupplier> model  = _supplierService.GetAllDto().ToList();
            return View(model);
        }

        public IActionResult AddSupplier()
        {
            DTOSupplier model = new DTOSupplier();
            model.Cities = _supplierService.GetCities().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return View("AddEditSupplier", model);
        }

        public IActionResult EditSupplier(int id)
        {
            DTOSupplier model = _supplierService.GetByIdDto(id);
            if (model == null)
                return RedirectToAction(nameof(Index));

            model.Cities = _supplierService.GetCities().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return View("AddEditSupplier", model);
        }

        public IActionResult SaveSupplier(DTOSupplier model)
        {
            if (model.Id == 0)
            {
                _supplierService.Add(model);

                var n = new Notification
                {
                    DateTime = DateTime.Now,
                    Text = $"New supplier - {model.Name}",
                    UserId = int.Parse(_userManager.GetUserId(HttpContext.User))
                };
                _notification.Create(n);
            }
                      
            else
                _supplierService.Edit(model);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteSupplier(int id)
        {

            Supplier supplier = _supplierService.GetById(id);
            if (supplier != null)
            {
                var n = new Notification
                {
                    DateTime = DateTime.Now,
                    Text = $"Removed supplier - {supplier.Name}",
                    UserId = int.Parse(_userManager.GetUserId(HttpContext.User))
                };
                _notification.Create(n);

                _supplierService.Delete(supplier);
            }
                

            return RedirectToAction(nameof(Index));
        }
    }


}