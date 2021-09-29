using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class ClassCheckInOutService : ServiceBase<ClassCheckInOut, ClassCheckInOutRepository>
    {
        public ClassCheckInOutService(ClassCheckInOutRepository repository)
        {
            this.Repository = repository;
        }
    }
}
