using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>
    {
        public CompanyRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public CompanyRepository()
        {
        }
    }
}
