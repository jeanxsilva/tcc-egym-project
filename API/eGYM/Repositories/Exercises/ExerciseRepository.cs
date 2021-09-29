using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class ExerciseRepository : RepositoryBase<Exercise>
    {
        public ExerciseRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ExerciseRepository()
        {
        }
    }
}
