using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class PaymentReversalStatusRepository : RepositoryBase<PaymentReversalStatus>
    {
        public PaymentReversalStatusRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentReversalStatusRepository()
        {
        }
    }
}
