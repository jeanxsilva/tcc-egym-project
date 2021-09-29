using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class PaymentTypeService : ServiceBase<PaymentType, PaymentTypeRepository>
    {
        public PaymentTypeService(PaymentTypeRepository repository)
        {
            this.Repository = repository;
        }
    }
}
