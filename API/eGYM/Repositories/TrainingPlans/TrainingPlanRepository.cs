using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class TrainingPlanRepository : RepositoryBase<TrainingPlan>
    {
        public TrainingPlanRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public TrainingPlanRepository()
        {
        }
    }
}
