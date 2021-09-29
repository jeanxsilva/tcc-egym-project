using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class PhysicalAssesmentScheduledService : ServiceBase<PhysicalAssesmentScheduled, PhysicalAssesmentScheduledRepository>
    {
        public PhysicalAssesmentScheduledService(PhysicalAssesmentScheduledRepository repository)
        {
            this.Repository = repository;
        }
    }
}
