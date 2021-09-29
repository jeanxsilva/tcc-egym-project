using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class CompanyUnitService : ServiceBase<CompanyUnit, CompanyUnitRepository>
    {
        public CompanyUnitService(CompanyUnitRepository repository)
        {
            this.Repository = repository;
        }
    }
}
