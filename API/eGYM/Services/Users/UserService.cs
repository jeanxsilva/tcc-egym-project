using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class UserService : ServiceBase<User, UserRepository>
    {
        public UserService(UserRepository repository)
        {
            this.Repository = repository;
        }
    }
}
