using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class ModalityClassRepository : RepositoryBase<ModalityClass>
    {
        public ModalityClassRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public ModalityClassRepository()
        {
        }
    }
}
