using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class CompanyUnitRepository : RepositoryBase<CompanyUnit>
    {
        public CompanyUnitRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public CompanyUnitRepository()
        {
        }
    }
}
