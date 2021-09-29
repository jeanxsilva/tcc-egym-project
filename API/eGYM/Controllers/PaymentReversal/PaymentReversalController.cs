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
    public partial class PaymentReversalController : CrudControllerBase<PaymentReversal, PaymentReversalService, PaymentReversalRepository>
    {
    
        #region Service
        
        protected override PaymentReversalService Service { get; set; }

        #endregion

        #region Constructor

        public PaymentReversalController(PaymentReversalService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()
            
        [HttpGet]
        [Authorize(Roles = "PaymentReversal.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "PaymentReversal.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentReversal.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(PaymentReversal entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentReversal.D")]
        [Route("Delete")]
        public new dynamic Delete(PaymentReversal entity)
        {
            return base.Delete(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentReversal.U")]
        [Route("Edit")]
        public dynamic Edit(PaymentReversal entity)
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
            return dataColumns;
        }

        #endregion
    }
}
