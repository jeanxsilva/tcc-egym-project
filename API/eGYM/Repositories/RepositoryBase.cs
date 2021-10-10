using Microsoft.EntityFrameworkCore;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntityBase
    {

        private readonly EGymDbContext dbContext;

        public RepositoryBase()
        {
        }
        public RepositoryBase(EGymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> GetById(long entityId)
        {
            return await this.dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == entityId);
        }

        public async Task<bool> Remove(TEntity entity)
        {
            var entityToRemove = await GetById(entity.Id);

            if (entityToRemove != null)
            {
                this.dbContext.Set<TEntity>().Remove(entityToRemove);
                await this.dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Remove(List<TEntity> entities)
        {
            using var transaction = await this.dbContext.Database.BeginTransactionAsync();

            try
            {
                foreach (TEntity entity in entities)
                {
                    var entityToRemove = await GetById(entity.Id);

                    if (entityToRemove != null)
                    {
                        this.dbContext.Set<TEntity>().Remove(entityToRemove);
                        await this.dbContext.SaveChangesAsync();
                    }
                }

                transaction.Commit();
                return true;
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                Console.WriteLine(exception);
                return false;
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.dbContext.Set<TEntity>();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var async = await this.dbContext.Set<TEntity>().AddAsync(entity);
            TEntity savedEntity = async.Entity;

            await this.dbContext.SaveChangesAsync();

            return savedEntity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var entityToUpdate = await GetById(entity.Id);

            if (entityToUpdate != null)
            {
                TEntity savedEntity = this.dbContext.Set<TEntity>().Update(entity).Entity;
                await this.dbContext.SaveChangesAsync();

                return savedEntity;
            }

            return null;
        }

        public async Task<TEntity> InsertOrUpdate(TEntity entity)
        {
            if (entity != null)
            {
                if (entity.Id == null || entity.Id == 0)
                {
                    return await this.Create(entity);
                }

                return await this.Update(entity);
            }

            return null;
        }

        public async Task<bool> InsertOrUpdate(List<TEntity> entities)
        {
            using var transaction = await this.dbContext.Database.BeginTransactionAsync();
            try
            {
                await this.dbContext.AddAsync(entities);

                transaction.Commit();

                return true;
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                Console.WriteLine(exception);
                return false;
            }
        }
        public IQueryable<TEntity> GetQuery()
        {
            return this.dbContext.Set<TEntity>().AsQueryable<TEntity>();
        }

        public DbSet<TEntity> GetDbSet()
        {
            return this.dbContext.Set<TEntity>();
        }
    }
}