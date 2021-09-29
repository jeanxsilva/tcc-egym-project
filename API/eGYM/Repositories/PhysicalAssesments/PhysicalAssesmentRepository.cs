using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class PhysicalAssesmentRepository : RepositoryBase<PhysicalAssesment>
    {
        public PhysicalAssesmentRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PhysicalAssesmentRepository()
        {
        }
    }
}
