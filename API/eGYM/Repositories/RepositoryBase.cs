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

        public async Task<TEntity> GetByIdAsNoTrackingAsync(long entityId)
        {
            return await this.dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == entityId);
        }
        public TEntity GetByIdAsNoTracking(long entityId)
        {
            return this.dbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(e => e.Id == entityId);
        }

        public async Task<TEntity> GetById(long entityId)
        {
            return await this.dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == entityId);
        }

        public bool Remove(TEntity entity)
        {
            var entityToRemove = this.GetByIdAsNoTracking(entity.Id);

            if (entityToRemove != null)
            {
                this.dbContext.Remove<TEntity>(entityToRemove);
                //this.dbContext.DetachLocal<TEntity>(entityToRemove, entity.Id);
                this.dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            var entityToRemove = await this.GetByIdAsNoTrackingAsync(entity.Id);

            if (entityToRemove != null)
            {
                this.dbContext.Remove<TEntity>(entityToRemove);
                //this.dbContext.DetachLocal<TEntity>(entityToRemove, entity.Id);
                this.dbContext.SaveChanges();

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
                    var entityToRemove = await this.GetByIdAsNoTrackingAsync(entity.Id);

                    if (entityToRemove != null)
                    {
                        this.dbContext.Set<TEntity>().Remove(entityToRemove);
                        //this.dbContext.DetachLocal<TEntity>(entityToRemove, entity.Id);
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

            //this.dbContext.DetachLocal<TEntity>(entity, entity.Id);
            await this.dbContext.SaveChangesAsync();

            return savedEntity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            this.dbContext.Update<TEntity>(entity);
            //this.dbContext.DetachLocal<TEntity>(entity, entity.Id);

            await this.dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> InsertOrUpdate(TEntity entity)
        {
            if (entity != null)
            {
                if (entity.Id == null || entity.Id == 0)
                {
                    return await this.Create(entity);
                }

                TEntity existent = await this.dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity.Id);

                if (existent != null)
                {
                    return await this.Update(entity);
                }
            }

            return null;
        }

        public async Task<bool> InsertOrUpdate(List<TEntity> entities)
        {
            using var transaction = await this.dbContext.Database.BeginTransactionAsync();
            try
            {
                List<TEntity> insertEntities = entities.Where(e => e.Id == 0).ToList();
                await this.dbContext.AddRangeAsync(insertEntities);

                List<TEntity> updateEntities = entities.Where(e => e.Id != 0).ToList();
                this.dbContext.UpdateRange(updateEntities);

                await this.dbContext.SaveChangesAsync();

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
        public DbContext GetDbContext()
        {
            return this.dbContext;
        }
    }
}