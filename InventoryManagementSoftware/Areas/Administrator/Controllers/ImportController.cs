using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Import;
using InventoryManagementSoftware.Web.Helpers.Dropdown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class ImportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDropdown _dropdown;


        public ImportController(IUnitOfWork unitOfWork, IMapper mapper, IDropdown dropdown)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dropdown = dropdown;
        }

        public IActionResult Index(string search = null, DateTime? date = null)
        {
            List<DTOImport> model = _unitOfWork.Imports.GetAllDto(search, date).ToList();

            foreach(var x in model)
            {
                x.Quantity = _unitOfWork.Imports.GetImportQuantity(x.Id);
                x.TotalPrice = _unitOfWork.Imports.GetImportPrice(x.Id);
            }

            return View(model);
        }

        public IActionResult AddImport()
        {
            DTOImport model = new DTOImport
            {
                Inventories = _unitOfWork.Inventories.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Suppliers = _unitOfWork.Suppliers.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Employees = _unitOfWork.Employees.GetAll()
                .Select(x => new SelectListItem(x.Username, x.Id.ToString())).ToList()
            };

            return View("AddImport", model);
        }

        public IActionResult EditImport(int id)
        {
            DTOImport model = _unitOfWork.Imports.GetByIdDto(id);
            model.Suppliers = _unitOfWork.Suppliers.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            model.ImportDetails = _unitOfWork.Imports.GetImportDetails(id);

            return View(model);
        }

        public IActionResult SaveImport(DTOImport model)
        {
            if (model.Id == 0)
                _unitOfWork.Imports.Add(model);
            else
                _unitOfWork.Imports.Edit(model);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteImport(int id)
        {
            Import Import = _unitOfWork.Imports.GetById(id);
            if (Import != null)
                _unitOfWork.Imports.Delete(Import);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            DTOImport model = _unitOfWork.Imports.GetDetailsByIdDto(id);
            if (model != null)
                return PartialView("Details", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IEnumerable<SelectListItem>> GetEmployees(int inventoryId)
        {
            return await _dropdown.EmployeesByInventoryId(inventoryId);
        }

    }
}
