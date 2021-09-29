using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class PaymentReversalStatusService : ServiceBase<PaymentReversalStatus, PaymentReversalStatusRepository>
    {
        public PaymentReversalStatusService(PaymentReversalStatusRepository repository)
        {
            this.Repository = repository;
        }
    }
}
