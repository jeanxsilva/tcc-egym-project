using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Database.Repositories
{
    public class EquipmentunitRepository : RepositoryBase<Equipmentunit>
    {
        public EquipmentunitRepository(EGymDbContext dbContext) : base(dbContext)
        {
        }

        public EquipmentunitRepository()
        {
        }
    }
}
