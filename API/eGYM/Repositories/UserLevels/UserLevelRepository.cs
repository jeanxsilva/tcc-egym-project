using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class UserLevelRepository : RepositoryBase<UserLevel>
    {
        public UserLevelRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserLevelRepository()
        {
        }
    }
}
