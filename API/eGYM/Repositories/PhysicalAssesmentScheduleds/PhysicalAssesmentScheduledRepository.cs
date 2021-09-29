using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class PhysicalAssesmentScheduledRepository : RepositoryBase<PhysicalAssesmentScheduled>
    {
        public PhysicalAssesmentScheduledRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PhysicalAssesmentScheduledRepository()
        {
        }
    }
}
