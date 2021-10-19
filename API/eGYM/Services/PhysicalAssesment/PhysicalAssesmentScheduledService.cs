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

        public PhysicalAssesmentScheduledService(PhysicalAssesmentScheduledRepository repository, StudentRegistrationService studentRegistrationService) : this(repository)
        {
            this.studentRegistrationService = studentRegistrationService;
        }

        public async Task<PhysicalAssesmentScheduled> PreSave(PhysicalAssesmentScheduled entity)
        {
            entity.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            entity.StudentRegistration = await this.studentRegistrationService.GetByIdAsync(entity.StudentRegistrationId);
            return entity;
        }
    }
}
