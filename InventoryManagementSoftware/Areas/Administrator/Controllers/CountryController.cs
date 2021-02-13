using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class CountryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryController(IUnitOfWork unitOfWork, ICountryService countryService)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Country> model = _unitOfWork.Countries.GetAll();
            return View(model);
        }
        public IActionResult Add()
        {
            Country model = new Country();
            return PartialView("AddEdit", model);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));


            Country country = _unitOfWork.Countries.GetById(id.Value);
            _unitOfWork.Countries.Edit(country);
            return PartialView("AddEdit", country);
        }
        public IActionResult Delete(int id)
        {
            _unitOfWork.Countries.Delete(id);
            return RedirectToAction(nameof(Index));
        }      
                
        public IActionResult Save(Country model)
        {
            if (model.Id == 0)
            {
                _unitOfWork.Countries.Add(model);
            }
            else
            {
                _unitOfWork.Countries.Edit(model);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}