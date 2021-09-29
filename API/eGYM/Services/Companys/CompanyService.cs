using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class CompanyService : ServiceBase<Company, CompanyRepository>
    {
        public CompanyService(CompanyRepository repository)
        {
            this.Repository = repository;
        }
    }
}
