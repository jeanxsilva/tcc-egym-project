using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class InvoiceStatusRepository : RepositoryBase<InvoiceStatus>
    {
        public InvoiceStatusRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public InvoiceStatusRepository()
        {
        }
    }
}
