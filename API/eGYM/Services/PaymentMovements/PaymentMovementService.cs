using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class PaymentMovementService : ServiceBase<PaymentMovement, PaymentMovementRepository>
    {
        public PaymentMovementService(PaymentMovementRepository repository)
        {
            this.Repository = repository;
        }
    }
}
