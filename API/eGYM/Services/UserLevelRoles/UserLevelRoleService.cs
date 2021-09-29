using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class UserLevelRoleService : ServiceBase<UserLevelRole, UserLevelRoleRepository>
    {
        public UserLevelRoleService(UserLevelRoleRepository repository)
        {
            this.Repository = repository;
        }
    }
}
