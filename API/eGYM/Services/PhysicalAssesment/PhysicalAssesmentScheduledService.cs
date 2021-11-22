using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PhysicalAssesmentScheduledService
    {
        private readonly StudentRegistrationService studentRegistrationService;
        private readonly RequestStatusRepository requestStatusRepository;
        private readonly UserService userService;

        public PhysicalAssesmentScheduledService(PhysicalAssesmentScheduledRepository repository, StudentRegistrationService studentRegistrationService, RequestStatusRepository requestStatusRepository, UserService userService) : this(repository)
        {
            this.studentRegistrationService = studentRegistrationService;
            this.requestStatusRepository = requestStatusRepository;
            this.userService = userService;
        }

        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();

            return dataColumns;
        }

        #endregion

        public override async Task PreSavingRoutine(PhysicalAssesmentScheduled entity)
        {
            entity.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            entity.StudentRegistration = await this.studentRegistrationService.GetByIdAsync(entity.StudentRegistrationId);
        }

        public async Task<bool> CancelSchedule(PhysicalAssesmentScheduled scheduled)
        {
            scheduled.WasCanceled = true;

            if(scheduled.StudentRequests.Count > 0)
            {
                foreach(StudentRequest studentRequest in scheduled.StudentRequests)
                {
                    studentRequest.WasCanceled = true;
                    studentRequest.ClosedByUser = await this.userService.ResolveUser();
                    studentRequest.RequestStatus = await this.requestStatusRepository.GetById((int)RequestStatusEnum.Canceled);
                }
            }

            await this.Repository.Update(scheduled);

            return true;
        }
    }
}