using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class RegistrationModalityClassService : ServiceBase<RegistrationModalityClass, RegistrationModalityClassRepository>
    {
        public RegistrationModalityClassService(RegistrationModalityClassRepository repository)
        {
            this.Repository = repository;
        }
    }
}
