using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public EmployeeRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public EmployeeRepository()
        {
        }
    }
}
