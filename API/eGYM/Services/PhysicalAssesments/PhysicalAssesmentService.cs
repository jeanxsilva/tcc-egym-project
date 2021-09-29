using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class PhysicalAssesmentService : ServiceBase<PhysicalAssesment, PhysicalAssesmentRepository>
    {
        public PhysicalAssesmentService(PhysicalAssesmentRepository repository)
        {
            this.Repository = repository;
        }
    }
}
