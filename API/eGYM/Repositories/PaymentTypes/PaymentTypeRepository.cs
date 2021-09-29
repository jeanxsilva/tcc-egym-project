using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class PaymentTypeRepository : RepositoryBase<PaymentType>
    {
        public PaymentTypeRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public PaymentTypeRepository()
        {
        }
    }
}
