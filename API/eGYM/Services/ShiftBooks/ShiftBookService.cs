using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class ShiftBookService : ServiceBase<ShiftBook, ShiftBookRepository>
    {
        public ShiftBookService(ShiftBookRepository repository)
        {
            this.Repository = repository;
        }
    }
}
