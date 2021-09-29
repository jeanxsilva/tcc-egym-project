using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class RequestCategoryService : ServiceBase<RequestCategory, RequestCategoryRepository>
    {
        public RequestCategoryService(RequestCategoryRepository repository)
        {
            this.Repository = repository;
        }
    }
}
