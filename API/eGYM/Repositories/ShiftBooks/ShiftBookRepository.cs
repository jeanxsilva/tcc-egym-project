using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class ShiftBookRepository : RepositoryBase<ShiftBook>
    {
        public ShiftBookRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ShiftBookRepository()
        {
        }
    }
}
