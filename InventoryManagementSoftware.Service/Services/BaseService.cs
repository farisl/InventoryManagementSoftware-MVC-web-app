using InventoryManagementSoftware.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryManagementSoftware.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {

        private readonly IMSContext _context;
        public BaseService(IMSContext context)
        {
            _context = context;
        }
        public virtual TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual bool Delete(int id)
        {
            try
            {
                TEntity model = _context.Set<TEntity>().Find(id);
                _context.Set<TEntity>().Remove(model);
                _context.Entry<TEntity>(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public virtual bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public virtual TEntity Edit(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
    }
}
