using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSoftware.Service.IServices
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Add(TEntity entity);
        TEntity Edit(TEntity entity);
        bool Delete(int id);
        bool Delete(TEntity entity);
        TEntity GetById(int id);
    }
}
