using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class PaymentReversalService : ServiceBase<PaymentReversal, PaymentReversalRepository>
    {
        public PaymentReversalService(PaymentReversalRepository repository)
        {
            this.Repository = repository;
        }
    }
}
