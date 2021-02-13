using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Export;
using InventoryManagementSoftware.Web.Helpers.Dropdown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class ExportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDropdown _dropdown;


        public ExportController(IUnitOfWork unitOfWork, IMapper mapper, IDropdown dropdown)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dropdown = dropdown;
        }

        public IActionResult Index(string search = null, DateTime? date = null)
        {
            List<DTOExport> model = _unitOfWork.Exports.GetAllDto(search, date).ToList();

            foreach(var x in model)
            {
                x.Quantity = _unitOfWork.Exports.GetExportQuantity(x.Id);
                x.TotalPrice = _unitOfWork.Exports.GetExportPrice(x.Id);
            }

            return View(model);
        }

        public IActionResult AddExport()
        {
            DTOExport model = new DTOExport
            {
                Inventories = _unitOfWork.Inventories.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Customers = _unitOfWork.Customers.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Employees = _unitOfWork.Employees.GetAll()
                .Select(x => new SelectListItem(x.Username, x.Id.ToString())).ToList()
            };

            return View("AddExport", model);
        }

        public IActionResult EditExport(int id)
        {
            DTOExport model = _unitOfWork.Exports.GetByIdDto(id);
            model.Customers = _unitOfWork.Customers.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            model.ExportDetails = _unitOfWork.Exports.GetExportDetails(id);

            return View(model);
        }

        public IActionResult SaveExport(DTOExport model)
        {
            if (model.Id == 0)
                _unitOfWork.Exports.Add(model);
            else
                _unitOfWork.Exports.Edit(model);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteExport(int id)
        {
            Export export = _unitOfWork.Exports.GetById(id);
            if (export != null)
                _unitOfWork.Exports.Delete(export);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            DTOExport model = _unitOfWork.Exports.GetDetailsByIdDto(id);
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
