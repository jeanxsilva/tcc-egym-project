using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{

    public abstract class ServiceBase<TEntity, TRepository> : IServiceBase<TEntity, TRepository> where TEntity : IEntityBase where TRepository : IRepositoryBase<TEntity>
    {
        #region Members 'Properties' :: Repository

        public TRepository Repository { get; set; }

        #endregion

        #region Members 'Methods' 

        public virtual List<TEntity> GetAll()
        {
            return this.Repository.GetAll().ToList();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await this.Repository.GetById(id);
        }

        public virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            return await this.Repository.InsertOrUpdate(entity);
        }

        public virtual async Task<bool> SaveAsync(List<TEntity> entities)
        {
            if (!entities.Any()) return true;

            return await this.Repository.InsertOrUpdate(entities);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            return await this.Repository.RemoveAsync(entity);
        }

        public virtual async Task<bool> Delete(List<TEntity> entities)
        {
            if (!entities.Any()) return true;

            return await this.Repository.Remove(entities);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return this.Repository.GetQuery();
        }


        public virtual async Task PreSavingRoutine(TEntity entity)
        {
        }

        public virtual async Task PostSavingRoutine(TEntity entity)
        {
        }

        public virtual async Task PreDeleteRoutine(TEntity entity)
        {
        }

        public virtual async Task PostDeleteRoutine(TEntity entity)
        {
        }

        public virtual async Task PreUpdateRoutine(TEntity entity)
        {
        }

        public virtual async Task PostUpdateRoutine(TEntity entity)
        {
        }

        public virtual List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();

            return dataColumns;
        }

        public bool Delete(TEntity entity)
        {
            return this.Repository.Remove(entity);
        }
        #endregion
    }
}
