using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class StudentRequestService : ServiceBase<StudentRequest, StudentRequestRepository>
    {
        public StudentRequestService(StudentRequestRepository repository)
        {
            this.Repository = repository;
        }
    }
}
