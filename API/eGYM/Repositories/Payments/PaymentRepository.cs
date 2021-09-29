using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class PaymentRepository : RepositoryBase<Payment>
    {
        public PaymentRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentRepository()
        {
        }
    }
}
