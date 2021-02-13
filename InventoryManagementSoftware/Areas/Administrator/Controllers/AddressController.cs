using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class AddressController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddressController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {                        
            List<AddressPreviewVM> model = _unitOfWork.Addresses.GetAll()
                .Select(a => new AddressPreviewVM
                {
                    Id = a.Id,
                    Name = a.Name,
                    City=a.City.Name
                }).ToList();
            return View(model);
        }

        public IActionResult Add()
        {
            AddressEditVM model = new AddressEditVM();
            model.Cities = _unitOfWork.Cities.GetAll().Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            return View("AddEdit", model);
        }

        public IActionResult Edit(int id)
        {          
            Address address = _unitOfWork.Addresses.GetById(id);
            if (address == null)            
                return RedirectToAction(nameof(Index));

            AddressEditVM model = address;
            model.Cities = _unitOfWork.Cities.GetAll().Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            return View("AddEdit", model);
        }

        public IActionResult Delete(int id)
        {
            _unitOfWork.Addresses.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Save(AddressEditVM input)
        {
            if (input.Id == 0)
                _unitOfWork.Addresses.Add(input);
            else 
                _unitOfWork.Addresses.Edit(input);
            
            return RedirectToAction(nameof(Index));
        }

    }
}