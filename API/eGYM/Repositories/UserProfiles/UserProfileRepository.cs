using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class UserProfileRepository : RepositoryBase<UserProfile>
    {
        public UserProfileRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserProfileRepository()
        {
        }
    }
}
