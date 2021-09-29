using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class StudentRegistrationService : ServiceBase<StudentRegistration, StudentRegistrationRepository>
    {
        public StudentRegistrationService(StudentRegistrationRepository repository)
        {
            this.Repository = repository;
        }
    }
}
