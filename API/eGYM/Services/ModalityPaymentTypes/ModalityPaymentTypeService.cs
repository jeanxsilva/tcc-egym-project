using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class ModalityPaymentTypeService : ServiceBase<ModalityPaymentType, ModalityPaymentTypeRepository>
    {
        public ModalityPaymentTypeService(ModalityPaymentTypeRepository repository)
        {
            this.Repository = repository;
        }
    }
}
