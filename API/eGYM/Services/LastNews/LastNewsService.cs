using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class LastNewsService : ServiceBase<LastNews, LastNewsRepository>
    {
        public LastNewsService(LastNewsRepository repository)
        {
            this.Repository = repository;
        }
    }
}
