using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class InventoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public InventoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Inventory
        public IActionResult Index()
        {
            List<Inventory> inventories = _unitOfWork.Inventories.GetAll().ToList();
            return View(inventories);
        }
        public IActionResult Add()
        {
            DTOInventory model = new DTOInventory();
            model.Cities = _unitOfWork.Cities.GetAll().Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            return PartialView("AddEdit", model);
        }
        public IActionResult Edit(int id)
        {
            DTOInventory model = _unitOfWork.Inventories.GetByIdDto(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            model.Cities = _unitOfWork.Cities.GetAll().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return PartialView("AddEdit", model);

        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Save(DTOInventory model)
        {

            if (model.Id == 0)
            {
                _unitOfWork.Inventories.Add(model);
            }
            else
            {
                _unitOfWork.Inventories.Edit(model);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Inventory inventory = _unitOfWork.Inventories.GetById(id);
            if (inventory == null)
            {
                return RedirectToAction(nameof(Index));
            }

            List<Department> departments = _unitOfWork.Departments.GetByInventoryId(id).ToList();
            foreach (Department d in departments)
            {
                List<Shelves> shelves = _unitOfWork.Shelves.GetByDepartmentId(d.Id).ToList();
                foreach (Shelves s in shelves)
                {
                    _unitOfWork.Shelves.Delete(s);
                }


                _unitOfWork.Departments.Delete(d); //Brise sve odjele prilikom brisanja njihovog skladista.
            }

            _unitOfWork.Addresses.Delete(inventory.Address);
            _unitOfWork.PhoneNumbers.Delete(inventory.PhoneNumber);
            _unitOfWork.Inventories.Delete(inventory);
            return RedirectToAction(nameof(Index));
        }

        #endregion



    }


}


