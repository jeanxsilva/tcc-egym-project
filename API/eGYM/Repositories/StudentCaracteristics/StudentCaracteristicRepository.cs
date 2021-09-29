using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class StudentCaracteristicRepository : RepositoryBase<StudentCaracteristic>
    {
        public StudentCaracteristicRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public StudentCaracteristicRepository()
        {
        }
    }
}
