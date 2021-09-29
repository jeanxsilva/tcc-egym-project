using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class EmployeeService : ServiceBase<Employee, EmployeeRepository>
    {
        public EmployeeService(EmployeeRepository repository)
        {
            this.Repository = repository;
        }
    }
}
