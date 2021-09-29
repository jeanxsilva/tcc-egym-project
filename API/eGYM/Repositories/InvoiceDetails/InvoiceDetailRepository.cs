using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class InvoiceDetailRepository : RepositoryBase<InvoiceDetail>
    {
        public InvoiceDetailRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public InvoiceDetailRepository()
        {
        }
    }
}
