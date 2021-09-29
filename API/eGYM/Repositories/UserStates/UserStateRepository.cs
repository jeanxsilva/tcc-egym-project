using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class UserStateRepository : RepositoryBase<UserState>
    {
        public UserStateRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserStateRepository()
        {
        }
    }
}
