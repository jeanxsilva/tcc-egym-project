using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class ModalityClassService
    {
        private readonly CompanyUnitService companyUnitService;
        private readonly EmployeeService employeeService;
        private readonly ModalityService modalityService;

        public ModalityClassService(ModalityClassRepository repository, CompanyUnitService companyUnitService, EmployeeService employeeService, ModalityService modalityService) : this(repository)
        {
            this.companyUnitService = companyUnitService;
            this.employeeService = employeeService;
            this.modalityService = modalityService;
        }
        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            
            dataColumns.Add(new DataColumn("modality.description", DataTypes.String, "Modalidade"));
            dataColumns.Add(new DataColumn("instructor.user.name", DataTypes.String, "Instrutor"));
            dataColumns.Add(new DataColumn("totalActiveMembers", DataTypes.String, "Membros ativos"));
            dataColumns.Add(new DataColumn("totalVacancies", DataTypes.String, "Total permitido"));

            return dataColumns;
        }

        #endregion

        public override async Task PreSavingRoutine(ModalityClass entity)
        {
            if (entity.CompanyUnitId != 0)
            {
                entity.CompanyUnit = await this.companyUnitService.GetByIdAsync(entity.CompanyUnitId);
            }
            else
            {
                entity.CompanyUnit = await this.companyUnitService.ResolveCompanyUnit();
            }

            if (entity.InstructorId != null)
            {
                entity.Instructor = await this.employeeService.GetByIdAsync((int)entity.InstructorId);
            }

            entity.Modality = await this.modalityService.GetByIdAsync(entity.ModalityId);
        }
    }
}