using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class UserLevelService : ServiceBase<UserLevel, UserLevelRepository>
    {
        public UserLevelService(UserLevelRepository repository)
        {
            this.Repository = repository;
        }
    }
}
