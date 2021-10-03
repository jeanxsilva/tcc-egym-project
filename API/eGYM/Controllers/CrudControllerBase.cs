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
    public abstract class CrudControllerBase<TEntity, TService, TRepository> where TEntity : class, IEntityBase,
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
            this.ReturnBag.HasError = false;

            if (entityId != null)
            {
                this.ReturnBag.Result = await this.Service.GetByIdAsync((int)entityId);
            }
            else
            {
                this.ReturnBag.Result = this.Service.GetAll();
            }

            return this.ReturnBag;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual dynamic Delete(TEntity entity)
        {
            this.ReturnBag.HasError = false;

            if (entity != null)
            {
                this.PreDeleteRoutine();

                this.ReturnBag.Result = this.Service.Delete(entity);

                this.PostDeleteRoutine();
            }
            else
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = "É necessário passar a entidade a ser excluida";
            }

            return this.ReturnBag;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<dynamic> SaveAsync(TEntity entity)
        {
            this.ReturnBag.HasError = false;

            if (entity != null)
            {
                this.PreSavingRoutine();

                this.ReturnBag.Result = await this.Service.SaveAsync(entity);

                this.PostSavingRoutine();
            }
            else
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = "É necessário passar a entidade a ser inserida";
            }

            return this.ReturnBag;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public virtual async Task<dynamic> UpdateAsync(TEntity entity)
        {
            this.ReturnBag.HasError = false;

            if (entity != null)
            {
                this.PreUpdateRoutine();

                this.ReturnBag.Result = await this.Service.SaveAsync(entity);

                this.PostUpdateRoutine();
            }
            else
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = "É necessário passar a entidade a ser atualizada";
            }

            return this.ReturnBag;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected virtual void PreSavingRoutine()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected virtual void PostSavingRoutine()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected virtual void PreDeleteRoutine()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected virtual void PostDeleteRoutine()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected virtual void PreUpdateRoutine()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected virtual void PostUpdateRoutine()
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected virtual List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("", DataTypes.String, ""));

            return dataColumns;
        }

        #endregion
    }

    public enum DataTypes
    {
        Int = 0,
        String = 1,
        Date = 2,
        Boolean = 3
    }

    public class DataColumn
    {
        public string PropertyName { get; set; }
        public string LabelDescription { get; set; }
        public DataTypes DataType { get; set; }

        public DataColumn(string name, DataTypes type, string description)
        {
            this.PropertyName = name;
            this.DataType = type;
            this.LabelDescription = description;
        }
    }
}