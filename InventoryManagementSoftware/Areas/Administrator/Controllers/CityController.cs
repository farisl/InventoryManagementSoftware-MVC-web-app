using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagementSoftware.ViewModels;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.IServices;
using InventoryManagementSoftware.Service;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{

    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class CityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityController(IUnitOfWork unitOfWork,ICountryService countryService)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<CityPreviewVM> model = _unitOfWork.Cities.GetAll()
                .Select(c => new CityPreviewVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    Country = c.Country.Name
                }).ToList();
            return View(model);
        }
        public IActionResult Add()
        {
            CityEditVM model = new CityEditVM();
            model.Countries = _unitOfWork.Countries.GetAll().Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            return PartialView("AddEdit", model);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            CityEditVM model = _unitOfWork.Cities.GetById(id.Value);
            IEnumerable<Country> countries = _unitOfWork.Countries.GetAll();
            model.Countries = countries.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return PartialView("AddEdit", model);
        }

        public IActionResult Delete(int id)
        {
            _unitOfWork.Cities.Delete(id);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Save(CityEditVM input)
        {
            City city;
            if (input.Id == 0)
            {
                city = new City();
                city = input;
                _unitOfWork.Cities.Add(city);
            }
            else
            {
                _unitOfWork.Cities.Edit(input);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}