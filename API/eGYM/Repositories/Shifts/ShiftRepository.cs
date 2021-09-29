using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class ShiftRepository : RepositoryBase<Shift>
    {
        public ShiftRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ShiftRepository()
        {
        }
    }
}
