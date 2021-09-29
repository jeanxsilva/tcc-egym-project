using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class ExerciseCategoryRepository : RepositoryBase<ExerciseCategory>
    {
        public ExerciseCategoryRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ExerciseCategoryRepository()
        {
        }
    }
}
