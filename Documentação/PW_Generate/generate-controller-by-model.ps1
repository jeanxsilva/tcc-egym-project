Set-Location C:\Users\jeanx\Documents\Git\TCC-Projeto\e-GYM\API\eGYM\GeneratedModels
$list = Get-ChildItem -Name

foreach($file in $list){
 $name = $file.replace('.cs','');
 $entityName = $file.replace('.cs','');

 New-Item -Path ../Controllers -Name $name -ItemType "directory"
 New-Item -Path ../Controllers/$name -Name $entityName"Controller.cs" -ItemType "file"
 
Add-Content -Path ../Controllers/$name/$entityName"Controller.cs" -Value @"
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
    public partial class $($entityName)Controller : CrudControllerBase<$entityName, $($entityName)Service, $($entityName)Repository>
    {
    
        #region Service
        
        protected override $($entityName)Service Service { get; set; }

        #endregion

        #region Constructor

        public $($entityName)Controller($($entityName)Service service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()
            
        [HttpGet]
        [Authorize(Roles = "$($entityName).R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "$($entityName).R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "$($entityName).C")]
        [Route("Insert")]
        public async Task<dynamic> Insert($($entityName) entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "$($entityName).D")]
        [Route("Delete")]
        public new dynamic Delete($($entityName) entity)
        {
            return base.Delete(entity);
        }

        [HttpPost]
        [Authorize(Roles = "$($entityName).U")]
        [Route("Edit")]
        public dynamic Edit($($entityName) entity)
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
"@
}
