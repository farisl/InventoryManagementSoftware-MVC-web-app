using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Customer;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSoftware.Areas.User.Controllers
{
    [Area("User")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notification;

        public CustomerController(ICustomerService customer, IMapper mapper,
            UserManager<ApplicationUser> userManager, INotificationService notification)
        {
            _customerService = customer;
            _mapper = mapper;
            _userManager = userManager;
            _notification = notification;
        }
        public IActionResult Index()
        {
            List<DTOCustomer> model = _customerService.GetAllDto().ToList();
            return View(model);
        }

        public IActionResult AddCustomer()
        {
            DTOCustomer model = new DTOCustomer();
            model.Cities = _customerService.GetCities().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return View("AddEditCustomer", model);
        }

        public IActionResult EditCustomer(int id)
        {
            DTOCustomer model = _customerService.GetByIdDto(id);
            if (model == null)
                return RedirectToAction(nameof(Index));

            model.Cities = _customerService.GetCities().Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return View("AddEditCustomer", model);
        }

        public IActionResult SaveCustomer(DTOCustomer model)
        {   
            if (model.Id == 0)
            {
                _customerService.Add(model);

                var n = new Notification
                {
                    DateTime = DateTime.Now,
                    Text = $"New customer - {model.Name}",
                    UserId = int.Parse(_userManager.GetUserId(HttpContext.User))
                };
                _notification.Create(n);
            }

            else
                _customerService.Edit(model);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteCustomer(int id)
        {           
            Customer customer = _customerService.GetById(id);
            var customerName = customer.Name;

            if (customer != null)
            {
                _customerService.Delete(customer);
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