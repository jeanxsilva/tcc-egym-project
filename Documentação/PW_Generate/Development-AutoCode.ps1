Set-Location C:\Users\jeanx\Documents\Git\TCC-Projeto\e-GYM\API\eGYM\Models
$list = Get-ChildItem -Name -File
    
# foreach($file in $list){
 
#     $entityName = $file.replace('.cs','');
#     (Get-Content ./$file).replace('public partial class '+ $entityName, 'public partial class ' + $entityName + ' : IEntityBase') | Set-Content ./$file
# }

New-Item -Path ../ -Name "(AutoCode)" -ItemType "directory"
New-Item -Path ../"(AutoCode)" -Name "GeneratedAutoCode.cs" -ItemType "file"

Add-Content -Path ../"(AutoCode)"/"GeneratedAutoCode.cs" -Value @"
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

namespace eGYM
{
"@
foreach($file in $list){
 
 $entityName = $file.replace('.cs','');
 
Add-Content -Path ../"(AutoCode)"/"GeneratedAutoCode.cs" -Value @"

    #region $($entityName)Service
    
    public partial class $($entityName)Service : ServiceBase<$($entityName), $($entityName)Repository>
    {
        public $($entityName)Service($($entityName)Repository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region $($entityName)Repository

    public partial class $($entityName)Repository : RepositoryBase<$($entityName)>
    {
        public $($entityName)Repository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public $($entityName)Repository()
        {
        }
    }

    #endregion

    #region $($entityName)Controller

    [Route("api/$($entityName)")]
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
        [Authorize(Roles = "$($entityName).C, $($entityName).U")]
        [Route("Save")]
        public async Task<dynamic> Save($($entityName) entity)
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

        #endregion

        #region GetDataColumns()
        
        [HttpGet]
        [Authorize(Roles = "$($entityName).R")]
        [Route("GetDataColumns")]
        public new List<DataColumn> GetDataColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            
            dataColumns = this.GetColumns();

            return dataColumns;
        }

        #endregion
    }

    #endregion
"@
}
Add-Content -Path ../"(AutoCode)"/"GeneratedAutoCode.cs" -Value @"
}
"@