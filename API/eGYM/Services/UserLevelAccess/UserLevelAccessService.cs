using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class UserLevelAccessService : ServiceBase<UserLevelAccess, UserLevelAccessRepository>
    {
        public UserLevelAccessService(UserLevelAccessRepository repository)
        {
            this.Repository = repository;
        }
    }
}
