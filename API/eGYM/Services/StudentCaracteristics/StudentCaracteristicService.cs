using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class StudentCaracteristicService : ServiceBase<StudentCaracteristic, StudentCaracteristicRepository>
    {
        public StudentCaracteristicService(StudentCaracteristicRepository repository)
        {
            this.Repository = repository;
        }
    }
}
