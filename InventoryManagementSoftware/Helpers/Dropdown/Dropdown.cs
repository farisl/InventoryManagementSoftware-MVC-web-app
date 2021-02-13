using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InventoryManagementSoftware.Service;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagementSoftware.Web.Helpers.Dropdown
{
    public class Dropdown : IDropdown
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Dropdown(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SelectListItem>> Attributes()
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Attributes.GetDropdownItems();
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }
        public async Task<IEnumerable<SelectListItem>> Inventories()
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Inventories.GetDropdownItems();
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }
        public async Task<IEnumerable<SelectListItem>> Departments()
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Departments.GetDropdownItems();
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }
        public async Task<IEnumerable<SelectListItem>> DepartmentsByInventoryId(int inventoryId)
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Departments.GetDropdownItemsByInventoryId(inventoryId);
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }
        public async Task<IEnumerable<SelectListItem>> Shelves()
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Shelves.GetDropdownItems();
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }
        public async Task<IEnumerable<SelectListItem>> ShelvesByDepartmentId(int departmentId)
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Shelves.GetDropdownItemsByDepartmentId(departmentId);
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }

        public async Task<IEnumerable<SelectListItem>> Brands()
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Brands.GetDropdownItems();
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }

        public async Task<IEnumerable<SelectListItem>> CategoriesByBrandId(int brandId)
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Categories.GetDropdownItemsByBrandId(brandId);
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }

        public async Task<IEnumerable<SelectListItem>> EmployeesByInventoryId(int inventoryId)
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Employees
                .GetDropdownItemsByInventoryId(inventoryId);
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }

        public async Task<IEnumerable<SelectListItem>> Products()
        {
            IEnumerable<DTODropdown> result = await _unitOfWork.Products.GetDropdownItems();
            return _mapper.Map<IEnumerable<SelectListItem>>(result);
        }

    }
}
