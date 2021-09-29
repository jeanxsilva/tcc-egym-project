using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eGYM.Database.Repositories;
using eGYM.Models;
using eGYM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ShiftController : CrudControllerBase<Shift, ShiftService, ShiftRepository>
    {
    
        #region Service
        
        protected override ShiftService Service { get; set; }

        #endregion

        #region Constructor

        public ShiftController(ShiftService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()
            
        [HttpGet]
        [Authorize(Roles = "Shift.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "Shift.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "Shift.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(Shift entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Shift.D")]
        [Route("Delete")]
        public new dynamic Delete(Shift entity)
        {
            return base.Delete(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Shift.U")]
        [Route("Edit")]
        public dynamic Edit(Shift entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        protected override void PreSavingRoutine()
        {
            base.PreSavingRoutine();
        }

        protected override void PostSavingRoutine()
        {
            base.PostSavingRoutine();
        }
        protected override void PreDeleteRoutine()
        {
            base.PreDeleteRoutine();
        }

        protected override void PostDeleteRoutine()
        {
            base.PostDeleteRoutine();
        }

        protected override void PreUpdateRoutine()
        {
            base.PreUpdateRoutine();
        }

        protected override void PostUpdateRoutine()
        {
            base.PostUpdateRoutine();
        }

        #endregion

        #region GetDataColumns()
        
        [HttpGet]
        [Route("GetDataColumns")]
        public List<DataColumn> GetDataColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("id",DataTypes.Int,"Id"));
            dataColumns.Add(new DataColumn("description", DataTypes.String, "Descrição"));
            return dataColumns;
        }

        #endregion
    }
}
