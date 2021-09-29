using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class ModalityService : ServiceBase<Modality, ModalityRepository>
    {
        public ModalityService(ModalityRepository repository)
        {
            this.Repository = repository;
        }
    }
}
