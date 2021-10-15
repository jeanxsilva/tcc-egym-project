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

    #region ClassCheckInOutService

    public partial class ClassCheckInOutService : ServiceBase<ClassCheckInOut, ClassCheckInOutRepository>
    {
        public ClassCheckInOutService(ClassCheckInOutRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region ClassCheckInOutRepository

    public partial class ClassCheckInOutRepository : RepositoryBase<ClassCheckInOut>
    {
        public ClassCheckInOutRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ClassCheckInOutRepository()
        {
        }
    }

    #endregion

    #region ClassCheckInOutController

    [Route("api/ClassCheckInOut")]
    [ApiController]
    public partial class ClassCheckInOutController : CrudControllerBase<ClassCheckInOut, ClassCheckInOutService, ClassCheckInOutRepository>
    {

        #region Service

        protected override ClassCheckInOutService Service { get; set; }

        #endregion

        #region Constructor

        public ClassCheckInOutController(ClassCheckInOutService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "ClassCheckInOut.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "ClassCheckInOut.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "ClassCheckInOut.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(ClassCheckInOut entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ClassCheckInOut.D")]
        [Route("Delete")]
        public new dynamic Delete(ClassCheckInOut entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ClassCheckInOut.U")]
        [Route("Edit")]
        public dynamic Edit(ClassCheckInOut entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "ClassCheckInOut.R")]
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

    #region CompanyService

    public partial class CompanyService : ServiceBase<Company, CompanyRepository>
    {
        public CompanyService(CompanyRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region CompanyRepository

    public partial class CompanyRepository : RepositoryBase<Company>
    {
        public CompanyRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public CompanyRepository()
        {
        }
    }

    #endregion

    #region CompanyController

    [Route("api/Company")]
    [ApiController]
    public partial class CompanyController : CrudControllerBase<Company, CompanyService, CompanyRepository>
    {

        #region Service

        protected override CompanyService Service { get; set; }

        #endregion

        #region Constructor

        public CompanyController(CompanyService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "Company.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "Company.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "Company.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(Company entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Company.D")]
        [Route("Delete")]
        public new dynamic Delete(Company entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Company.U")]
        [Route("Edit")]
        public dynamic Edit(Company entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "Company.R")]
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

    #region CompanyUnitService

    public partial class CompanyUnitService : ServiceBase<CompanyUnit, CompanyUnitRepository>
    {
        public CompanyUnitService(CompanyUnitRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region CompanyUnitRepository

    public partial class CompanyUnitRepository : RepositoryBase<CompanyUnit>
    {
        public CompanyUnitRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public CompanyUnitRepository()
        {
        }
    }

    #endregion

    #region CompanyUnitController

    [Route("api/CompanyUnit")]
    [ApiController]
    public partial class CompanyUnitController : CrudControllerBase<CompanyUnit, CompanyUnitService, CompanyUnitRepository>
    {

        #region Service

        protected override CompanyUnitService Service { get; set; }

        #endregion

        #region Constructor

        public CompanyUnitController(CompanyUnitService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "CompanyUnit.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "CompanyUnit.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "CompanyUnit.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(CompanyUnit entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "CompanyUnit.D")]
        [Route("Delete")]
        public new dynamic Delete(CompanyUnit entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "CompanyUnit.U")]
        [Route("Edit")]
        public dynamic Edit(CompanyUnit entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "CompanyUnit.R")]
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

    #region EmployeeService

    public partial class EmployeeService : ServiceBase<Employee, EmployeeRepository>
    {
        public EmployeeService(EmployeeRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region EmployeeRepository

    public partial class EmployeeRepository : RepositoryBase<Employee>
    {
        public EmployeeRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public EmployeeRepository()
        {
        }
    }

    #endregion

    #region EmployeeController

    [Route("api/Employee")]
    [ApiController]
    public partial class EmployeeController : CrudControllerBase<Employee, EmployeeService, EmployeeRepository>
    {

        #region Service

        protected override EmployeeService Service { get; set; }

        #endregion

        #region Constructor

        public EmployeeController(EmployeeService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "Employee.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "Employee.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "Employee.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(Employee entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Employee.D")]
        [Route("Delete")]
        public new dynamic Delete(Employee entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Employee.U")]
        [Route("Edit")]
        public dynamic Edit(Employee entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "Employee.R")]
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

    #region ExerciseService

    public partial class ExerciseService : ServiceBase<Exercise, ExerciseRepository>
    {
        public ExerciseService(ExerciseRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region ExerciseRepository

    public partial class ExerciseRepository : RepositoryBase<Exercise>
    {
        public ExerciseRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ExerciseRepository()
        {
        }
    }

    #endregion

    #region ExerciseController

    [Route("api/Exercise")]
    [ApiController]
    public partial class ExerciseController : CrudControllerBase<Exercise, ExerciseService, ExerciseRepository>
    {

        #region Service

        protected override ExerciseService Service { get; set; }

        #endregion

        #region Constructor

        public ExerciseController(ExerciseService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "Exercise.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "Exercise.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "Exercise.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(Exercise entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Exercise.D")]
        [Route("Delete")]
        public new dynamic Delete(Exercise entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Exercise.U")]
        [Route("Edit")]
        public dynamic Edit(Exercise entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "Exercise.R")]
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

    #region ExerciseCategoryService

    public partial class ExerciseCategoryService : ServiceBase<ExerciseCategory, ExerciseCategoryRepository>
    {
        public ExerciseCategoryService(ExerciseCategoryRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region ExerciseCategoryRepository

    public partial class ExerciseCategoryRepository : RepositoryBase<ExerciseCategory>
    {
        public ExerciseCategoryRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ExerciseCategoryRepository()
        {
        }
    }

    #endregion

    #region ExerciseCategoryController

    [Route("api/ExerciseCategory")]
    [ApiController]
    public partial class ExerciseCategoryController : CrudControllerBase<ExerciseCategory, ExerciseCategoryService, ExerciseCategoryRepository>
    {

        #region Service

        protected override ExerciseCategoryService Service { get; set; }

        #endregion

        #region Constructor

        public ExerciseCategoryController(ExerciseCategoryService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "ExerciseCategory.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "ExerciseCategory.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "ExerciseCategory.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(ExerciseCategory entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ExerciseCategory.D")]
        [Route("Delete")]
        public new dynamic Delete(ExerciseCategory entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ExerciseCategory.U")]
        [Route("Edit")]
        public dynamic Edit(ExerciseCategory entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "ExerciseCategory.R")]
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

    #region InvoiceService

    public partial class InvoiceService : ServiceBase<Invoice, InvoiceRepository>
    {
        public InvoiceService(InvoiceRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region InvoiceRepository

    public partial class InvoiceRepository : RepositoryBase<Invoice>
    {
        public InvoiceRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public InvoiceRepository()
        {
        }
    }

    #endregion

    #region InvoiceController

    [Route("api/Invoice")]
    [ApiController]
    public partial class InvoiceController : CrudControllerBase<Invoice, InvoiceService, InvoiceRepository>
    {

        #region Service

        protected override InvoiceService Service { get; set; }

        #endregion

        #region Constructor

        public InvoiceController(InvoiceService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "Invoice.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "Invoice.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "Invoice.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(Invoice entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Invoice.D")]
        [Route("Delete")]
        public new dynamic Delete(Invoice entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Invoice.U")]
        [Route("Edit")]
        public dynamic Edit(Invoice entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "Invoice.R")]
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

    #region InvoiceDetailService

    public partial class InvoiceDetailService : ServiceBase<InvoiceDetail, InvoiceDetailRepository>
    {
        public InvoiceDetailService(InvoiceDetailRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region InvoiceDetailRepository

    public partial class InvoiceDetailRepository : RepositoryBase<InvoiceDetail>
    {
        public InvoiceDetailRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public InvoiceDetailRepository()
        {
        }
    }

    #endregion

    #region InvoiceDetailController

    [Route("api/InvoiceDetail")]
    [ApiController]
    public partial class InvoiceDetailController : CrudControllerBase<InvoiceDetail, InvoiceDetailService, InvoiceDetailRepository>
    {

        #region Service

        protected override InvoiceDetailService Service { get; set; }

        #endregion

        #region Constructor

        public InvoiceDetailController(InvoiceDetailService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "InvoiceDetail.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "InvoiceDetail.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "InvoiceDetail.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(InvoiceDetail entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "InvoiceDetail.D")]
        [Route("Delete")]
        public new dynamic Delete(InvoiceDetail entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "InvoiceDetail.U")]
        [Route("Edit")]
        public dynamic Edit(InvoiceDetail entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "InvoiceDetail.R")]
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

    #region InvoiceStatusService

    public partial class InvoiceStatusService : ServiceBase<InvoiceStatus, InvoiceStatusRepository>
    {
        public InvoiceStatusService(InvoiceStatusRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region InvoiceStatusRepository

    public partial class InvoiceStatusRepository : RepositoryBase<InvoiceStatus>
    {
        public InvoiceStatusRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public InvoiceStatusRepository()
        {
        }
    }

    #endregion

    #region InvoiceStatusController

    [Route("api/InvoiceStatus")]
    [ApiController]
    public partial class InvoiceStatusController : CrudControllerBase<InvoiceStatus, InvoiceStatusService, InvoiceStatusRepository>
    {

        #region Service

        protected override InvoiceStatusService Service { get; set; }

        #endregion

        #region Constructor

        public InvoiceStatusController(InvoiceStatusService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "InvoiceStatus.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "InvoiceStatus.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "InvoiceStatus.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(InvoiceStatus entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "InvoiceStatus.D")]
        [Route("Delete")]
        public new dynamic Delete(InvoiceStatus entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "InvoiceStatus.U")]
        [Route("Edit")]
        public dynamic Edit(InvoiceStatus entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "InvoiceStatus.R")]
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

    #region LastNewsService

    public partial class LastNewsService : ServiceBase<LastNews, LastNewsRepository>
    {
        public LastNewsService(LastNewsRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region LastNewsRepository

    public partial class LastNewsRepository : RepositoryBase<LastNews>
    {
        public LastNewsRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public LastNewsRepository()
        {
        }
    }

    #endregion

    #region LastNewsController

    [Route("api/LastNews")]
    [ApiController]
    public partial class LastNewsController : CrudControllerBase<LastNews, LastNewsService, LastNewsRepository>
    {

        #region Service

        protected override LastNewsService Service { get; set; }

        #endregion

        #region Constructor

        public LastNewsController(LastNewsService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "LastNews.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "LastNews.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "LastNews.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(LastNews entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "LastNews.D")]
        [Route("Delete")]
        public new dynamic Delete(LastNews entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "LastNews.U")]
        [Route("Edit")]
        public dynamic Edit(LastNews entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "LastNews.R")]
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

    #region ModalityService

    public partial class ModalityService : ServiceBase<Modality, ModalityRepository>
    {
        public ModalityService(ModalityRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region ModalityRepository

    public partial class ModalityRepository : RepositoryBase<Modality>
    {
        public ModalityRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ModalityRepository()
        {
        }
    }

    #endregion

    #region ModalityController

    [Route("api/Modality")]
    [ApiController]
    public partial class ModalityController : CrudControllerBase<Modality, ModalityService, ModalityRepository>
    {

        #region Service

        protected override ModalityService Service { get; set; }

        #endregion

        #region Constructor

        public ModalityController(ModalityService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "Modality.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "Modality.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "Modality.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(Modality entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Modality.D")]
        [Route("Delete")]
        public new dynamic Delete(Modality entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Modality.U")]
        [Route("Edit")]
        public dynamic Edit(Modality entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "Modality.R")]
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

    #region ModalityClassService

    public partial class ModalityClassService : ServiceBase<ModalityClass, ModalityClassRepository>
    {
        public ModalityClassService(ModalityClassRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region ModalityClassRepository

    public partial class ModalityClassRepository : RepositoryBase<ModalityClass>
    {
        public ModalityClassRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ModalityClassRepository()
        {
        }
    }

    #endregion

    #region ModalityClassController

    [Route("api/ModalityClass")]
    [ApiController]
    public partial class ModalityClassController : CrudControllerBase<ModalityClass, ModalityClassService, ModalityClassRepository>
    {

        #region Service

        protected override ModalityClassService Service { get; set; }

        #endregion

        #region Constructor

        public ModalityClassController(ModalityClassService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "ModalityClass.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "ModalityClass.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "ModalityClass.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(ModalityClass entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ModalityClass.D")]
        [Route("Delete")]
        public new dynamic Delete(ModalityClass entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ModalityClass.U")]
        [Route("Edit")]
        public dynamic Edit(ModalityClass entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "ModalityClass.R")]
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

    #region ModalityPaymentTypeService

    public partial class ModalityPaymentTypeService : ServiceBase<ModalityPaymentType, ModalityPaymentTypeRepository>
    {
        public ModalityPaymentTypeService(ModalityPaymentTypeRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region ModalityPaymentTypeRepository

    public partial class ModalityPaymentTypeRepository : RepositoryBase<ModalityPaymentType>
    {
        public ModalityPaymentTypeRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ModalityPaymentTypeRepository()
        {
        }
    }

    #endregion

    #region ModalityPaymentTypeController

    [Route("api/ModalityPaymentType")]
    [ApiController]
    public partial class ModalityPaymentTypeController : CrudControllerBase<ModalityPaymentType, ModalityPaymentTypeService, ModalityPaymentTypeRepository>
    {

        #region Service

        protected override ModalityPaymentTypeService Service { get; set; }

        #endregion

        #region Constructor

        public ModalityPaymentTypeController(ModalityPaymentTypeService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "ModalityPaymentType.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "ModalityPaymentType.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "ModalityPaymentType.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(ModalityPaymentType entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ModalityPaymentType.D")]
        [Route("Delete")]
        public new dynamic Delete(ModalityPaymentType entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ModalityPaymentType.U")]
        [Route("Edit")]
        public dynamic Edit(ModalityPaymentType entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "ModalityPaymentType.R")]
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

    #region PaymentService

    public partial class PaymentService : ServiceBase<Payment, PaymentRepository>
    {
        public PaymentService(PaymentRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region PaymentRepository

    public partial class PaymentRepository : RepositoryBase<Payment>
    {
        public PaymentRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentRepository()
        {
        }
    }

    #endregion

    #region PaymentController

    [Route("api/Payment")]
    [ApiController]
    public partial class PaymentController : CrudControllerBase<Payment, PaymentService, PaymentRepository>
    {

        #region Service

        protected override PaymentService Service { get; set; }

        #endregion

        #region Constructor

        public PaymentController(PaymentService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "Payment.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "Payment.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "Payment.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(Payment entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Payment.D")]
        [Route("Delete")]
        public new dynamic Delete(Payment entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Payment.U")]
        [Route("Edit")]
        public dynamic Edit(Payment entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "Payment.R")]
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

    #region PaymentMovementService

    public partial class PaymentMovementService : ServiceBase<PaymentMovement, PaymentMovementRepository>
    {
        public PaymentMovementService(PaymentMovementRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region PaymentMovementRepository

    public partial class PaymentMovementRepository : RepositoryBase<PaymentMovement>
    {
        public PaymentMovementRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentMovementRepository()
        {
        }
    }

    #endregion

    #region PaymentMovementController

    [Route("api/PaymentMovement")]
    [ApiController]
    public partial class PaymentMovementController : CrudControllerBase<PaymentMovement, PaymentMovementService, PaymentMovementRepository>
    {

        #region Service

        protected override PaymentMovementService Service { get; set; }

        #endregion

        #region Constructor

        public PaymentMovementController(PaymentMovementService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "PaymentMovement.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "PaymentMovement.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentMovement.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(PaymentMovement entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentMovement.D")]
        [Route("Delete")]
        public new dynamic Delete(PaymentMovement entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentMovement.U")]
        [Route("Edit")]
        public dynamic Edit(PaymentMovement entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "PaymentMovement.R")]
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

    #region PaymentReversalService

    public partial class PaymentReversalService : ServiceBase<PaymentReversal, PaymentReversalRepository>
    {
        public PaymentReversalService(PaymentReversalRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region PaymentReversalRepository

    public partial class PaymentReversalRepository : RepositoryBase<PaymentReversal>
    {
        public PaymentReversalRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentReversalRepository()
        {
        }
    }

    #endregion

    #region PaymentReversalController

    [Route("api/PaymentReversal")]
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
            return base.DeleteAsync(entity);
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

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "PaymentReversal.R")]
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

    #region PaymentReversalStatusService

    public partial class PaymentReversalStatusService : ServiceBase<PaymentReversalStatus, PaymentReversalStatusRepository>
    {
        public PaymentReversalStatusService(PaymentReversalStatusRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region PaymentReversalStatusRepository

    public partial class PaymentReversalStatusRepository : RepositoryBase<PaymentReversalStatus>
    {
        public PaymentReversalStatusRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentReversalStatusRepository()
        {
        }
    }

    #endregion

    #region PaymentReversalStatusController

    [Route("api/PaymentReversalStatus")]
    [ApiController]
    public partial class PaymentReversalStatusController : CrudControllerBase<PaymentReversalStatus, PaymentReversalStatusService, PaymentReversalStatusRepository>
    {

        #region Service

        protected override PaymentReversalStatusService Service { get; set; }

        #endregion

        #region Constructor

        public PaymentReversalStatusController(PaymentReversalStatusService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "PaymentReversalStatus.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "PaymentReversalStatus.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentReversalStatus.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(PaymentReversalStatus entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentReversalStatus.D")]
        [Route("Delete")]
        public new dynamic Delete(PaymentReversalStatus entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentReversalStatus.U")]
        [Route("Edit")]
        public dynamic Edit(PaymentReversalStatus entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "PaymentReversalStatus.R")]
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

    #region PaymentTypeService

    public partial class PaymentTypeService : ServiceBase<PaymentType, PaymentTypeRepository>
    {
        public PaymentTypeService(PaymentTypeRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region PaymentTypeRepository

    public partial class PaymentTypeRepository : RepositoryBase<PaymentType>
    {
        public PaymentTypeRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentTypeRepository()
        {
        }
    }

    #endregion

    #region PaymentTypeController

    [Route("api/PaymentType")]
    [ApiController]
    public partial class PaymentTypeController : CrudControllerBase<PaymentType, PaymentTypeService, PaymentTypeRepository>
    {

        #region Service

        protected override PaymentTypeService Service { get; set; }

        #endregion

        #region Constructor

        public PaymentTypeController(PaymentTypeService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "PaymentType.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "PaymentType.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentType.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(PaymentType entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentType.D")]
        [Route("Delete")]
        public new dynamic Delete(PaymentType entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PaymentType.U")]
        [Route("Edit")]
        public dynamic Edit(PaymentType entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "PaymentType.R")]
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

    #region PhysicalAssesmentService

    public partial class PhysicalAssesmentService : ServiceBase<PhysicalAssesment, PhysicalAssesmentRepository>
    {
        public PhysicalAssesmentService(PhysicalAssesmentRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region PhysicalAssesmentRepository

    public partial class PhysicalAssesmentRepository : RepositoryBase<PhysicalAssesment>
    {
        public PhysicalAssesmentRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PhysicalAssesmentRepository()
        {
        }
    }

    #endregion

    #region PhysicalAssesmentController

    [Route("api/PhysicalAssesment")]
    [ApiController]
    public partial class PhysicalAssesmentController : CrudControllerBase<PhysicalAssesment, PhysicalAssesmentService, PhysicalAssesmentRepository>
    {

        #region Service

        protected override PhysicalAssesmentService Service { get; set; }

        #endregion

        #region Constructor

        public PhysicalAssesmentController(PhysicalAssesmentService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "PhysicalAssesment.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "PhysicalAssesment.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "PhysicalAssesment.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(PhysicalAssesment entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PhysicalAssesment.D")]
        [Route("Delete")]
        public new dynamic Delete(PhysicalAssesment entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PhysicalAssesment.U")]
        [Route("Edit")]
        public dynamic Edit(PhysicalAssesment entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "PhysicalAssesment.R")]
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

    #region PhysicalAssesmentScheduledService

    public partial class PhysicalAssesmentScheduledService : ServiceBase<PhysicalAssesmentScheduled, PhysicalAssesmentScheduledRepository>
    {
        public PhysicalAssesmentScheduledService(PhysicalAssesmentScheduledRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region PhysicalAssesmentScheduledRepository

    public partial class PhysicalAssesmentScheduledRepository : RepositoryBase<PhysicalAssesmentScheduled>
    {
        public PhysicalAssesmentScheduledRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PhysicalAssesmentScheduledRepository()
        {
        }
    }

    #endregion

    #region PhysicalAssesmentScheduledController

    [Route("api/PhysicalAssesmentScheduled")]
    [ApiController]
    public partial class PhysicalAssesmentScheduledController : CrudControllerBase<PhysicalAssesmentScheduled, PhysicalAssesmentScheduledService, PhysicalAssesmentScheduledRepository>
    {

        #region Service

        protected override PhysicalAssesmentScheduledService Service { get; set; }

        #endregion

        #region Constructor

        public PhysicalAssesmentScheduledController(PhysicalAssesmentScheduledService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "PhysicalAssesmentScheduled.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "PhysicalAssesmentScheduled.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "PhysicalAssesmentScheduled.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(PhysicalAssesmentScheduled entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PhysicalAssesmentScheduled.D")]
        [Route("Delete")]
        public new dynamic Delete(PhysicalAssesmentScheduled entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "PhysicalAssesmentScheduled.U")]
        [Route("Edit")]
        public dynamic Edit(PhysicalAssesmentScheduled entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "PhysicalAssesmentScheduled.R")]
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

    #region RegistrationModalityClassService

    public partial class RegistrationModalityClassService : ServiceBase<RegistrationModalityClass, RegistrationModalityClassRepository>
    {
        public RegistrationModalityClassService(RegistrationModalityClassRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region RegistrationModalityClassRepository

    public partial class RegistrationModalityClassRepository : RepositoryBase<RegistrationModalityClass>
    {
        public RegistrationModalityClassRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public RegistrationModalityClassRepository()
        {
        }
    }

    #endregion

    #region RegistrationModalityClassController

    [Route("api/RegistrationModalityClass")]
    [ApiController]
    public partial class RegistrationModalityClassController : CrudControllerBase<RegistrationModalityClass, RegistrationModalityClassService, RegistrationModalityClassRepository>
    {

        #region Service

        protected override RegistrationModalityClassService Service { get; set; }

        #endregion

        #region Constructor

        public RegistrationModalityClassController(RegistrationModalityClassService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "RegistrationModalityClass.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "RegistrationModalityClass.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "RegistrationModalityClass.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(RegistrationModalityClass entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "RegistrationModalityClass.D")]
        [Route("Delete")]
        public new dynamic Delete(RegistrationModalityClass entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "RegistrationModalityClass.U")]
        [Route("Edit")]
        public dynamic Edit(RegistrationModalityClass entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "RegistrationModalityClass.R")]
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

    #region RequestCategoryService

    public partial class RequestCategoryService : ServiceBase<RequestCategory, RequestCategoryRepository>
    {
        public RequestCategoryService(RequestCategoryRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region RequestCategoryRepository

    public partial class RequestCategoryRepository : RepositoryBase<RequestCategory>
    {
        public RequestCategoryRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public RequestCategoryRepository()
        {
        }
    }

    #endregion

    #region RequestCategoryController

    [Route("api/RequestCategory")]
    [ApiController]
    public partial class RequestCategoryController : CrudControllerBase<RequestCategory, RequestCategoryService, RequestCategoryRepository>
    {

        #region Service

        protected override RequestCategoryService Service { get; set; }

        #endregion

        #region Constructor

        public RequestCategoryController(RequestCategoryService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "RequestCategory.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "RequestCategory.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "RequestCategory.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(RequestCategory entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "RequestCategory.D")]
        [Route("Delete")]
        public new dynamic Delete(RequestCategory entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "RequestCategory.U")]
        [Route("Edit")]
        public dynamic Edit(RequestCategory entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "RequestCategory.R")]
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

    #region RequestStatusService

    public partial class RequestStatusService : ServiceBase<RequestStatus, RequestStatusRepository>
    {
        public RequestStatusService(RequestStatusRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region RequestStatusRepository

    public partial class RequestStatusRepository : RepositoryBase<RequestStatus>
    {
        public RequestStatusRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public RequestStatusRepository()
        {
        }
    }

    #endregion

    #region RequestStatusController

    [Route("api/RequestStatus")]
    [ApiController]
    public partial class RequestStatusController : CrudControllerBase<RequestStatus, RequestStatusService, RequestStatusRepository>
    {

        #region Service

        protected override RequestStatusService Service { get; set; }

        #endregion

        #region Constructor

        public RequestStatusController(RequestStatusService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "RequestStatus.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "RequestStatus.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "RequestStatus.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(RequestStatus entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "RequestStatus.D")]
        [Route("Delete")]
        public new dynamic Delete(RequestStatus entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "RequestStatus.U")]
        [Route("Edit")]
        public dynamic Edit(RequestStatus entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "RequestStatus.R")]
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

    #region ShiftService

    public partial class ShiftService : ServiceBase<Shift, ShiftRepository>
    {
        public ShiftService(ShiftRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region ShiftRepository

    public partial class ShiftRepository : RepositoryBase<Shift>
    {
        public ShiftRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ShiftRepository()
        {
        }
    }

    #endregion

    #region ShiftController

    [Route("api/Shift")]
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
            return base.DeleteAsync(entity);
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

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "Shift.R")]
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

    #region ShiftBookService

    public partial class ShiftBookService : ServiceBase<ShiftBook, ShiftBookRepository>
    {
        public ShiftBookService(ShiftBookRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region ShiftBookRepository

    public partial class ShiftBookRepository : RepositoryBase<ShiftBook>
    {
        public ShiftBookRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ShiftBookRepository()
        {
        }
    }

    #endregion

    #region ShiftBookController

    [Route("api/ShiftBook")]
    [ApiController]
    public partial class ShiftBookController : CrudControllerBase<ShiftBook, ShiftBookService, ShiftBookRepository>
    {

        #region Service

        protected override ShiftBookService Service { get; set; }

        #endregion

        #region Constructor

        public ShiftBookController(ShiftBookService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "ShiftBook.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "ShiftBook.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "ShiftBook.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(ShiftBook entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ShiftBook.D")]
        [Route("Delete")]
        public new dynamic Delete(ShiftBook entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "ShiftBook.U")]
        [Route("Edit")]
        public dynamic Edit(ShiftBook entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "ShiftBook.R")]
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

    #region StudentCaracteristicService

    public partial class StudentCaracteristicService : ServiceBase<StudentCaracteristic, StudentCaracteristicRepository>
    {
        public StudentCaracteristicService(StudentCaracteristicRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region StudentCaracteristicRepository

    public partial class StudentCaracteristicRepository : RepositoryBase<StudentCaracteristic>
    {
        public StudentCaracteristicRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public StudentCaracteristicRepository()
        {
        }
    }

    #endregion

    #region StudentCaracteristicController

    [Route("api/StudentCaracteristic")]
    [ApiController]
    public partial class StudentCaracteristicController : CrudControllerBase<StudentCaracteristic, StudentCaracteristicService, StudentCaracteristicRepository>
    {

        #region Service

        protected override StudentCaracteristicService Service { get; set; }

        #endregion

        #region Constructor

        public StudentCaracteristicController(StudentCaracteristicService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "StudentCaracteristic.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "StudentCaracteristic.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "StudentCaracteristic.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(StudentCaracteristic entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "StudentCaracteristic.D")]
        [Route("Delete")]
        public new dynamic Delete(StudentCaracteristic entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "StudentCaracteristic.U")]
        [Route("Edit")]
        public dynamic Edit(StudentCaracteristic entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "StudentCaracteristic.R")]
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

    #region StudentRegistrationService

    public partial class StudentRegistrationService : ServiceBase<StudentRegistration, StudentRegistrationRepository>
    {
        public StudentRegistrationService(StudentRegistrationRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region StudentRegistrationRepository

    public partial class StudentRegistrationRepository : RepositoryBase<StudentRegistration>
    {
        public StudentRegistrationRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public StudentRegistrationRepository()
        {
        }
    }

    #endregion

    #region StudentRegistrationController

    [Route("api/StudentRegistration")]
    [ApiController]
    public partial class StudentRegistrationController : CrudControllerBase<StudentRegistration, StudentRegistrationService, StudentRegistrationRepository>
    {

        #region Service

        protected override StudentRegistrationService Service { get; set; }

        #endregion

        #region Constructor

        public StudentRegistrationController(StudentRegistrationService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "StudentRegistration.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "StudentRegistration.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "StudentRegistration.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(StudentRegistration entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "StudentRegistration.D")]
        [Route("Delete")]
        public new dynamic Delete(StudentRegistration entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "StudentRegistration.U")]
        [Route("Edit")]
        public dynamic Edit(StudentRegistration entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "StudentRegistration.R")]
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

    #region StudentRequestService

    public partial class StudentRequestService : ServiceBase<StudentRequest, StudentRequestRepository>
    {
        public StudentRequestService(StudentRequestRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region StudentRequestRepository

    public partial class StudentRequestRepository : RepositoryBase<StudentRequest>
    {
        public StudentRequestRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public StudentRequestRepository()
        {
        }
    }

    #endregion

    #region StudentRequestController

    [Route("api/StudentRequest")]
    [ApiController]
    public partial class StudentRequestController : CrudControllerBase<StudentRequest, StudentRequestService, StudentRequestRepository>
    {

        #region Service

        protected override StudentRequestService Service { get; set; }

        #endregion

        #region Constructor

        public StudentRequestController(StudentRequestService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "StudentRequest.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "StudentRequest.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "StudentRequest.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(StudentRequest entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "StudentRequest.D")]
        [Route("Delete")]
        public new dynamic Delete(StudentRequest entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "StudentRequest.U")]
        [Route("Edit")]
        public dynamic Edit(StudentRequest entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "StudentRequest.R")]
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

    #region TrainingPlanService

    public partial class TrainingPlanService : ServiceBase<TrainingPlan, TrainingPlanRepository>
    {
        public TrainingPlanService(TrainingPlanRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region TrainingPlanRepository

    public partial class TrainingPlanRepository : RepositoryBase<TrainingPlan>
    {
        public TrainingPlanRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public TrainingPlanRepository()
        {
        }
    }

    #endregion

    #region TrainingPlanController

    [Route("api/TrainingPlan")]
    [ApiController]
    public partial class TrainingPlanController : CrudControllerBase<TrainingPlan, TrainingPlanService, TrainingPlanRepository>
    {

        #region Service

        protected override TrainingPlanService Service { get; set; }

        #endregion

        #region Constructor

        public TrainingPlanController(TrainingPlanService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "TrainingPlan.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "TrainingPlan.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingPlan.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(TrainingPlan entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingPlan.D")]
        [Route("Delete")]
        public new dynamic Delete(TrainingPlan entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingPlan.U")]
        [Route("Edit")]
        public dynamic Edit(TrainingPlan entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "TrainingPlan.R")]
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

    #region TrainingPlanExerciseService

    public partial class TrainingPlanExerciseService : ServiceBase<TrainingPlanExercise, TrainingPlanExerciseRepository>
    {
        public TrainingPlanExerciseService(TrainingPlanExerciseRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region TrainingPlanExerciseRepository

    public partial class TrainingPlanExerciseRepository : RepositoryBase<TrainingPlanExercise>
    {
        public TrainingPlanExerciseRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public TrainingPlanExerciseRepository()
        {
        }
    }

    #endregion

    #region TrainingPlanExerciseController

    [Route("api/TrainingPlanExercise")]
    [ApiController]
    public partial class TrainingPlanExerciseController : CrudControllerBase<TrainingPlanExercise, TrainingPlanExerciseService, TrainingPlanExerciseRepository>
    {

        #region Service

        protected override TrainingPlanExerciseService Service { get; set; }

        #endregion

        #region Constructor

        public TrainingPlanExerciseController(TrainingPlanExerciseService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "TrainingPlanExercise.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "TrainingPlanExercise.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingPlanExercise.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(TrainingPlanExercise entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingPlanExercise.D")]
        [Route("Delete")]
        public new dynamic Delete(TrainingPlanExercise entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingPlanExercise.U")]
        [Route("Edit")]
        public dynamic Edit(TrainingPlanExercise entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "TrainingPlanExercise.R")]
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

    #region UserService

    public partial class UserService : ServiceBase<User, UserRepository>
    {
        public UserService(UserRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region UserRepository

    public partial class UserRepository : RepositoryBase<User>
    {
        public UserRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserRepository()
        {
        }
    }

    #endregion

    #region UserController

    [Route("api/User")]
    [ApiController]
    public partial class UserController : CrudControllerBase<User, UserService, UserRepository>
    {

        #region Service

        protected override UserService Service { get; set; }

        #endregion

        #region Constructor

        public UserController(UserService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "User.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "User.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "User.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(User entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "User.D")]
        [Route("Delete")]
        public new dynamic Delete(User entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "User.U")]
        [Route("Edit")]
        public dynamic Edit(User entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "User.R")]
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

    #region UserLevelService

    public partial class UserLevelService : ServiceBase<UserLevel, UserLevelRepository>
    {
        public UserLevelService(UserLevelRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region UserLevelRepository

    public partial class UserLevelRepository : RepositoryBase<UserLevel>
    {
        public UserLevelRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserLevelRepository()
        {
        }
    }

    #endregion

    #region UserLevelController

    [Route("api/UserLevel")]
    [ApiController]
    public partial class UserLevelController : CrudControllerBase<UserLevel, UserLevelService, UserLevelRepository>
    {

        #region Service

        protected override UserLevelService Service { get; set; }

        #endregion

        #region Constructor

        public UserLevelController(UserLevelService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "UserLevel.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "UserLevel.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "UserLevel.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(UserLevel entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserLevel.D")]
        [Route("Delete")]
        public new dynamic Delete(UserLevel entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserLevel.U")]
        [Route("Edit")]
        public dynamic Edit(UserLevel entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "UserLevel.R")]
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

    #region UserLevelAccessService

    public partial class UserLevelAccessService : ServiceBase<UserLevelAccess, UserLevelAccessRepository>
    {
        public UserLevelAccessService(UserLevelAccessRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region UserLevelAccessRepository

    public partial class UserLevelAccessRepository : RepositoryBase<UserLevelAccess>
    {
        public UserLevelAccessRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserLevelAccessRepository()
        {
        }
    }

    #endregion

    #region UserLevelAccessController

    [Route("api/UserLevelAccess")]
    [ApiController]
    public partial class UserLevelAccessController : CrudControllerBase<UserLevelAccess, UserLevelAccessService, UserLevelAccessRepository>
    {

        #region Service

        protected override UserLevelAccessService Service { get; set; }

        #endregion

        #region Constructor

        public UserLevelAccessController(UserLevelAccessService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "UserLevelAccess.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "UserLevelAccess.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "UserLevelAccess.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(UserLevelAccess entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserLevelAccess.D")]
        [Route("Delete")]
        public new dynamic Delete(UserLevelAccess entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserLevelAccess.U")]
        [Route("Edit")]
        public dynamic Edit(UserLevelAccess entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "UserLevelAccess.R")]
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

    #region UserLevelRoleService

    public partial class UserLevelRoleService : ServiceBase<UserLevelRole, UserLevelRoleRepository>
    {
        public UserLevelRoleService(UserLevelRoleRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region UserLevelRoleRepository

    public partial class UserLevelRoleRepository : RepositoryBase<UserLevelRole>
    {
        public UserLevelRoleRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserLevelRoleRepository()
        {
        }
    }

    #endregion

    #region UserLevelRoleController

    [Route("api/UserLevelRole")]
    [ApiController]
    public partial class UserLevelRoleController : CrudControllerBase<UserLevelRole, UserLevelRoleService, UserLevelRoleRepository>
    {

        #region Service

        protected override UserLevelRoleService Service { get; set; }

        #endregion

        #region Constructor

        public UserLevelRoleController(UserLevelRoleService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "UserLevelRole.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "UserLevelRole.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "UserLevelRole.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(UserLevelRole entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserLevelRole.D")]
        [Route("Delete")]
        public new dynamic Delete(UserLevelRole entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserLevelRole.U")]
        [Route("Edit")]
        public dynamic Edit(UserLevelRole entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "UserLevelRole.R")]
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

    #region UserProfileService

    public partial class UserProfileService : ServiceBase<UserProfile, UserProfileRepository>
    {
        public UserProfileService(UserProfileRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region UserProfileRepository

    public partial class UserProfileRepository : RepositoryBase<UserProfile>
    {
        public UserProfileRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserProfileRepository()
        {
        }
    }

    #endregion

    #region UserProfileController

    [Route("api/UserProfile")]
    [ApiController]
    public partial class UserProfileController : CrudControllerBase<UserProfile, UserProfileService, UserProfileRepository>
    {

        #region Service

        protected override UserProfileService Service { get; set; }

        #endregion

        #region Constructor

        public UserProfileController(UserProfileService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "UserProfile.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "UserProfile.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "UserProfile.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(UserProfile entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserProfile.D")]
        [Route("Delete")]
        public new dynamic Delete(UserProfile entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserProfile.U")]
        [Route("Edit")]
        public dynamic Edit(UserProfile entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "UserProfile.R")]
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

    #region UserStateService

    public partial class UserStateService : ServiceBase<UserState, UserStateRepository>
    {
        public UserStateService(UserStateRepository repository)
        {
            this.Repository = repository;
        }
    }

    #endregion

    #region UserStateRepository

    public partial class UserStateRepository : RepositoryBase<UserState>
    {
        public UserStateRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserStateRepository()
        {
        }
    }

    #endregion

    #region UserStateController

    [Route("api/UserState")]
    [ApiController]
    public partial class UserStateController : CrudControllerBase<UserState, UserStateService, UserStateRepository>
    {

        #region Service

        protected override UserStateService Service { get; set; }

        #endregion

        #region Constructor

        public UserStateController(UserStateService service)
        {
            this.Service = service;
        }

        #endregion

        #region CRUD :: List(), GetById(), Insert(), Edit(), Remove()

        [HttpGet]
        [Authorize(Roles = "UserState.R")]
        [Route("List")]
        public async Task<dynamic> List()
        {
            return await base.List(null);
        }

        [HttpGet]
        [Authorize(Roles = "UserState.R")]
        [Route("GetById")]
        public async Task<dynamic> GetById(int entityId)
        {
            return await base.List(entityId);
        }

        [HttpPost]
        [Authorize(Roles = "UserState.C")]
        [Route("Insert")]
        public async Task<dynamic> Insert(UserState entity)
        {
            return await base.SaveAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserState.D")]
        [Route("Delete")]
        public new dynamic Delete(UserState entity)
        {
            return base.DeleteAsync(entity);
        }

        [HttpPost]
        [Authorize(Roles = "UserState.U")]
        [Route("Edit")]
        public dynamic Edit(UserState entity)
        {
            return base.UpdateAsync(entity);
        }

        #endregion

        #region Pre's and Post's routines

        #endregion

        #region GetDataColumns()

        [HttpGet]
        [Authorize(Roles = "UserState.R")]
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
}  