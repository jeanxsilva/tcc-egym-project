using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class InvoiceRepository : RepositoryBase<Invoice>
    {
        public InvoiceRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public InvoiceRepository()
        {
        }
    }
}
