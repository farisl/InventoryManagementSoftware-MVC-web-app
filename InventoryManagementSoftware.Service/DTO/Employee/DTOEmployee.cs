using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Address;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.DTO.Employee
{
    public class DTOEmployee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Gender Gender { get; set; }
        public int GenderId { get; set; }
        public string JMBG { get; set; }
        public DateTime BirthDate { get; set; }
        public DTOAddress Address { get; set; }
        public int AddressId { get; set; }
        public double Salary { get; set; }
        public int? InventoryId { get; set; }
        public string Inventory { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string FullName { get; set; }
        public int Age { get; set; }

        public List<SelectListItem> Genders { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Inventories { get; set; }
    }
}
