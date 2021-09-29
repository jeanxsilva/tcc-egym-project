using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class ModalityRepository : RepositoryBase<Modality>
    {
        public ModalityRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ModalityRepository()
        {
        }
    }
}
