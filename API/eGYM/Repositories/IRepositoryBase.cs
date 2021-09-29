using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public interface IRepositoryBase<TEntity>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(long entityId);
        Task<bool> Remove(TEntity entity);
        Task<bool> Remove(List<TEntity> entities);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> InsertOrUpdate(TEntity entity);
        Task<bool> InsertOrUpdate(List<TEntity> entities);
        Task<TEntity> Update(TEntity entity);
        IQueryable<TEntity> GetQuery();
    }
}