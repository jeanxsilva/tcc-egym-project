using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class LastNewsRepository : RepositoryBase<LastNews>
    {
        public LastNewsRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public LastNewsRepository()
        {
        }
    }
}
