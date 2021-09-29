using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class TrainingPlanExerciseRepository : RepositoryBase<TrainingPlanExercise>
    {
        public TrainingPlanExerciseRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public TrainingPlanExerciseRepository()
        {
        }
    }
}
