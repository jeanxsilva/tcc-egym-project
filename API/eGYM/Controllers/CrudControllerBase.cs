using eGYM.Database.Repositories;
using eGYM.Models;
using eGYM.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eGYM
{
    public abstract class CrudControllerBase<TEntity, TService, TRepository> : ControllerBase where TEntity : class, IEntityBase,
        new() where TRepository : IRepositoryBase<TEntity>,
        new() where TService : IServiceBase<TEntity, TRepository>
    {
        #region Fields

        protected abstract TService Service { get; set; }
        public dynamic ReturnBag { get; set; }

        #endregion

        #region Constructor

        public CrudControllerBase()
        {
            this.ReturnBag = new ExpandoObject();
        }

        #endregion

        #region Members
        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<dynamic> List(int? entityId)
        {
            try
            {
                this.ReturnBag.HasError = false;

                if (entityId != null)
                {
                    this.ReturnBag.Result = await this.Service.GetByIdAsync((int)entityId);
                }
                else
                {
                    this.ReturnBag.Result = this.Service.GetAll();
                }
            }
            catch (Exception exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;
            }

            return this.ReturnBag;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<dynamic> DeleteAsync(TEntity entity)
        {
            try
            {
                if (entity != null)
                {
                    await this.Service.PreDeleteRoutine(entity);

                    this.ReturnBag.Result = this.Service.DeleteAsync(entity);

                    await this.Service.PostDeleteRoutine(entity);
                }
                else
                {
                    throw new Exception("É necessário passar a entidade a ser excluida");
                }
            }
            catch (Exception exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;
            }

            return this.ReturnBag;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<dynamic> SaveAsync(TEntity entity)
        {
            try
            {
                if (entity != null)
                {
                    await this.Service.PreSavingRoutine(entity);

                    TEntity savedEntity = await this.Service.SaveAsync(entity);

                    if (savedEntity != null)
                    {
                        this.ReturnBag.Result = true;
                    }

                    await this.Service.PostSavingRoutine(entity);
                }
                else
                {
                    throw new Exception("É necessário passar a entidade a ser inserida");
                }
            }
            catch (Exception exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;
            }

            return this.ReturnBag;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<dynamic> UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity != null)
                {
                    await this.Service.PreUpdateRoutine(entity);

                    this.ReturnBag.Result = await this.Service.SaveAsync(entity);

                    await this.Service.PostUpdateRoutine(entity);
                }
                else
                {
                    throw new Exception("É necessário passar a entidade a ser atualizada");
                }
            }
            catch (Exception exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;
            }

            return this.ReturnBag;
        }

        #endregion
    }
}