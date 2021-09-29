using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class RequestCategoryRepository : RepositoryBase<RequestCategory>
    {
        public RequestCategoryRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public RequestCategoryRepository()
        {
        }
    }
}
