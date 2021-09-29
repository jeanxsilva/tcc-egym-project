using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class PaymentReversalRepository : RepositoryBase<PaymentReversal>
    {
        public PaymentReversalRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentReversalRepository()
        {
        }
    }
}
