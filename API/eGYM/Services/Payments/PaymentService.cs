using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class PaymentService : ServiceBase<Payment, PaymentRepository>
    {
        public PaymentService(PaymentRepository repository)
        {
            this.Repository = repository;
        }
    }
}
