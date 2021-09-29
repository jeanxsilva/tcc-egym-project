using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class ModalityClassService : ServiceBase<ModalityClass, ModalityClassRepository>
    {
        public ModalityClassService(ModalityClassRepository repository)
        {
            this.Repository = repository;
        }
    }
}
