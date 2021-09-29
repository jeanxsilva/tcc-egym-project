using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class UserLevelAccessRepository : RepositoryBase<UserLevelAccess>
    {
        public UserLevelAccessRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserLevelAccessRepository()
        {
        }
    }
}
