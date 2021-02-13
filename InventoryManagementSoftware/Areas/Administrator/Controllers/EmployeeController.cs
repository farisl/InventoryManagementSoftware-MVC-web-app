using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index(string search = null)
        {
            List<DTOEmployee> model = _unitOfWork.Employees.GetAllDto(search).ToList();
            foreach(var x in model)
            {
                x.FullName = x.FirstName + " " + x.LastName;
                x.Age = DateTime.Now.Year - x.BirthDate.Year;
                Inventory inventory = _unitOfWork.Employees.GetInventory(x.Id);
                if (inventory == null)
                    x.Inventory = "X";
                else
                    x.Inventory = inventory.Name;
            }

            return View(model);
        }

        public IActionResult AddEmployee()
        {
            DTOEmployee model = new DTOEmployee();
            model.Cities = _unitOfWork.Cities.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            model.Genders = _unitOfWork.Employees.GetGenders()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            model.Inventories = _unitOfWork.Inventories.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

            return View("AddEditEmployee", model);
        }

        public IActionResult EditEmployee(int id)
        {
            Employee employee = _unitOfWork.Employees.GetById(id);
            DTOEmployee model = _mapper.Map<DTOEmployee>(employee);
            Inventory inventory = _unitOfWork.Employees.GetInventory(id);
            model.InventoryId = inventory?.Id;
            //if (inventory == null)
            //    model.InventoryId = null;
            //else
            //    model.InventoryId = inventory.Id;
            model.Salary = _unitOfWork.Employees.GetSalary(id);
            model.Cities = _unitOfWork.Cities.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            model.Genders = _unitOfWork.Employees.GetGenders()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            model.Inventories = _unitOfWork.Inventories.GetAll()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            if(model.InventoryId != null)
                model.Inventories.Insert(0, new SelectListItem("--Choose inventory--", "null"));

            return View("AddEditEmployee", model);
        }

        public IActionResult SaveEmployee(DTOEmployee model)
        {
            if (model.Id == 0)
                _unitOfWork.Employees.Add(model);
            else
                _unitOfWork.Employees.Edit(model);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteEmployee(int id)
        {
            Employee employee = _unitOfWork.Employees.GetById(id);
            if (employee != null)
                _unitOfWork.Employees.Delete(employee);

            return RedirectToAction(nameof(Index));
        }
        

    }
}
