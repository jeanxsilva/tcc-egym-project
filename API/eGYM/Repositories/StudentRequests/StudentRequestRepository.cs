using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class StudentRequestRepository : RepositoryBase<StudentRequest>
    {
        public StudentRequestRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public StudentRequestRepository()
        {
        }
    }
}
