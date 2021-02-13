using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Shelves;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class ShelvesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ShelvesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index(int DepartmentID) // InventoryID prima ukoliko je poziv za pregled odjela odredjenog skladiste
        {
            IEnumerable<DTOShelves> model;

            if (DepartmentID > 0)
            {
                model = _unitOfWork.Shelves.GetByDepartmentIdDto(DepartmentID);
            }
            else
            {
                model = _unitOfWork.Shelves.GetAllDto().ToList();
            }

            return View(model);
        }

        public IActionResult Add(int DepartmentID)
        {
            DTOShelves model = new DTOShelves();

            if (DepartmentID > 0)
            {
                model.Department = _unitOfWork.Departments.GetByIdDto(DepartmentID);
            }
            model.Inventories = _unitOfWork.Inventories.GetAll().Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToList();
            model.Departments = _unitOfWork.Departments.GetAll().Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

            return PartialView("AddEdit", model);
        }

        public IActionResult Edit(int id)
        {

            DTOShelves model = _unitOfWork.Shelves.GetByIdDto(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            model.Inventories = _unitOfWork.Inventories.GetAll().Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToList();
            model.Departments = _unitOfWork.Departments.GetAll().Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToList();
            return PartialView("AddEdit", model);

        }

        public IActionResult Save(DTOShelves model)
        {
            if (model.Id == 0)
            {
                _unitOfWork.Shelves.Add(_mapper.Map<Shelves>(model));
            }
            else
            {
                _unitOfWork.Shelves.Edit(_mapper.Map<Shelves>(model));
            }
            return RedirectToAction(nameof(Index)); // PREPRAVITI NA Index Departmenta
        }

        public IActionResult Delete(int id)
        {
            Shelves shelves = _unitOfWork.Shelves.GetById(id);
            if (shelves == null)
            {
                return RedirectToAction(nameof(Index));
            }
            _unitOfWork.Shelves.Delete(shelves);
            return RedirectToAction(nameof(Index));

        }
    }
}