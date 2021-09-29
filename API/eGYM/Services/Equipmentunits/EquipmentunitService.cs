using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class EquipmentunitService : ServiceBase<Equipmentunit, EquipmentunitRepository>
    {
        public EquipmentunitService(EquipmentunitRepository repository)
        {
            this.Repository = repository;
        }
    }
}
