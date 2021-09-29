using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class PaymentMovementRepository : RepositoryBase<PaymentMovement>
    {
        public PaymentMovementRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentMovementRepository()
        {
        }
    }
}
