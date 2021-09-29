using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class ModalityPaymentTypeRepository : RepositoryBase<ModalityPaymentType>
    {
        public ModalityPaymentTypeRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ModalityPaymentTypeRepository()
        {
        }
    }
}
