using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class StudentRegistrationRepository : RepositoryBase<StudentRegistration>
    {
        public StudentRegistrationRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public StudentRegistrationRepository()
        {
        }
    }
}
