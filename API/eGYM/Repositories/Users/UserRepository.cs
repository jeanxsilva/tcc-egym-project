using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public UserRepository()
        {
        }
    }
}
