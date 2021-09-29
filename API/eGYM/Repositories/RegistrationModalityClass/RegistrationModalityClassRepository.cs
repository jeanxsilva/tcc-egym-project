using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class RegistrationModalityClassRepository : RepositoryBase<RegistrationModalityClass>
    {
        public RegistrationModalityClassRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public RegistrationModalityClassRepository()
        {
        }
    }
}
