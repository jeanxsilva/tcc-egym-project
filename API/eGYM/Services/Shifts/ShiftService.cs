using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class ShiftService : ServiceBase<Shift, ShiftRepository>
    {
        public ShiftService(ShiftRepository repository)
        {
            this.Repository = repository;
        }
    }
}
