using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Customer;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administrator")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notification;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper,
            UserManager<ApplicationUser> userManager, INotificationService notification)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _notification = notification;
        }
        public IActionResult Index(string search = null)
        {
            List<Service.DTO.Customer.DTOCustomer> model;
            model = _unitOfWork.Customers.GetAllDto(search).ToList();
            return View(model);
        }

        public IActionResult AddCustomer()
        {
            DTOCustomer model = new DTOCustomer();
            model.Cities = _unitOfWork.Customers.GetCities().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return View("AddEditCustomer", model);
        }

        public IActionResult EditCustomer(int id)
        {            
            Customer customer = _unitOfWork.Customers.GetById(id);
            DTOCustomer model = new DTOCustomer();
            if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            model = _mapper.Map<DTOCustomer>(customer);
            model.Cities = _unitOfWork.Customers.GetCities().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return View("AddEditCustomer", model);
        }

        public IActionResult SaveCustomer(DTOCustomer model)
        {   
            if (model.Id == 0)
            {
                _unitOfWork.Customers.Add(model);

                var n = new Notification
                {
                    DateTime = DateTime.Now,
                    Text = $"New customer - {model.Name}",
                    UserId = int.Parse(_userManager.GetUserId(HttpContext.User))
                };
                _notification.Create(n);
            }
            else
            {
                _unitOfWork.Customers.Edit(model);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteCustomer(int id)
        {           
            Customer customer = _unitOfWork.Customers.GetById(id);            

            if (customer!=null)
            {
                var customerName = customer.Name;
                _unitOfWork.Customers.Delete(customer);

                var n = new Notification
                {
                    DateTime = DateTime.Now,
                    Text = $"Removed customer - {customerName}",
                    UserId = int.Parse(_userManager.GetUserId(HttpContext.User))
                };
                _notification.Create(n);
            }
            return RedirectToAction(nameof(Index));
        }
    }


}