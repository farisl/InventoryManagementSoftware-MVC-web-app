using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // InventoryID prima ukoliko je poziv za pregled odjela odredjenog skladiste
        public IActionResult Index(int InventoryID) {
            List<DTODepartment> model;
            if (InventoryID > 0)
                model = _unitOfWork.Departments.GetByInventoryIdDto(InventoryID).ToList();
            else
                model = _unitOfWork.Departments.GetAllDto().ToList();

            return View(model);
        }

        public IActionResult Add(int InventoryId)  // InventoryId setuje Inventory ukoliko se direktno iz skladista poziva dodavanje odjela 
        {
            DTODepartment model = new DTODepartment();
            if (InventoryId > 0)
                model.Inventory = _unitOfWork.Inventories.GetByIdDto(InventoryId);

            model.Inventories = _unitOfWork.Inventories.GetAll().Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToList();
            return PartialView("AddEdit", model);
        }

        public IActionResult Edit(int id)
        {
            DTODepartment model = _unitOfWork.Departments.GetByIdDto(id);
            if (model == null)
                return RedirectToAction(nameof(Index));

            model.Inventories = _unitOfWork.Inventories.GetAll().Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToList();

            return PartialView("AddEdit", model);
        }

        public IActionResult Save(DTODepartment model)
        {
            Department department = _mapper.Map<Department>(model);
            if (model.Id == 0)
                _unitOfWork.Departments.Add(department);

            else
                _unitOfWork.Departments.Edit(_mapper.Map<Department>(model));

            return RedirectToAction(nameof(Index)); // Implementirati REDIREKCIJu NA INDEX Skladista 

        }

        public IActionResult DeleteDepartment(int id)
        {
            Department department = _unitOfWork.Departments.GetById(id);
            if (department != null)
                _unitOfWork.Departments.Delete(department);

            return RedirectToAction(nameof(Index));
        }
    }
}