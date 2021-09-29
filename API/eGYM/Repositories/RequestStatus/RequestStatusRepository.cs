using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class RequestStatusRepository : RepositoryBase<RequestStatus>
    {
        public RequestStatusRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public RequestStatusRepository()
        {
        }
    }
}
