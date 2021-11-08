using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public interface IServiceBase<TEntity, TRepository> where TEntity : IEntityBase where TRepository : IRepositoryBase <TEntity>
    {
        List<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> SaveAsync(TEntity entity);

        Task<bool> SaveAsync(List<TEntity> entities);
        
        Task<bool> DeleteAsync(TEntity entity);
        bool Delete(TEntity entity);

        Task<bool>Delete(List<TEntity> entities);

        Task PreSavingRoutine(TEntity entity);

        Task PostSavingRoutine(TEntity entity);

        Task PreDeleteRoutine(TEntity entity);

        Task PostDeleteRoutine(TEntity entity);

        Task PreUpdateRoutine(TEntity entity);

        Task PostUpdateRoutine(TEntity entity);

        List<DataColumn> GetColumns();
    }
}