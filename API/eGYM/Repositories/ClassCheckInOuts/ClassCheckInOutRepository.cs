using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class ClassCheckInOutRepository : RepositoryBase<ClassCheckInOut>
    {
        public ClassCheckInOutRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ClassCheckInOutRepository()
        {
        }
    }
}
