using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InventoryManagementSoftware.Core.Models;
using InventoryManagementSoftware.Service.DTO.Attribute;
using InventoryManagementSoftware.Service.DTO.Dropdown;
using InventoryManagementSoftware.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSoftware.Service.Services
{
    public class AttributeService : BaseService<Attribute>, IAttributeService
    {
        private readonly IMSContext _context;
        private readonly IMapper _mapper;
        public AttributeService(IMSContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(DTOAttribute model)
        {
            try
            {
                Attribute attribute = _mapper.Map<Attribute>(model);
                _context.Add(attribute);
                _context.SaveChanges();
            }
            catch(System.Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(DTOAttribute model)
        {
            try
            {
                Attribute attribute = _mapper.Map<Attribute>(model);
                _context.Update(attribute);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOAttribute> GetAllDto()
        {
            var result = _context.Attributes;

            return _mapper.Map<List<DTOAttribute>>(result);
        }

        public DTOAttribute GetByIdDto(int id)
        {
            var result = _context.Attributes.Find(id);

            return _mapper.Map<DTOAttribute>(result);
        }

        public async Task<IEnumerable<DTODropdown>> GetDropdownItems()
        {
            return await _context.Attributes.Select(x => new DTODropdown
            {
                Id = x.Id.ToString(),
                Value = x.Name
            }).ToListAsync();
        }
    }
}
