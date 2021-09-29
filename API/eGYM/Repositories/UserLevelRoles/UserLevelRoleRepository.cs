using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class UserLevelRoleRepository : RepositoryBase<UserLevelRole>
    {
        public UserLevelRoleRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserLevelRoleRepository()
        {
        }
    }
}
