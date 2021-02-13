using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Attribute;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace InventoryManagementSoftware.Service.DTO.Product
{
    public class DTOProductAttribute
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public DTOAttribute Attribute { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }
    }
}
