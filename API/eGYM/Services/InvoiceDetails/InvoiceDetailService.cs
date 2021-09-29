using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class InvoiceDetailService : ServiceBase<InvoiceDetail, InvoiceDetailRepository>
    {
        public InvoiceDetailService(InvoiceDetailRepository repository)
        {
            this.Repository = repository;
        }
    }
}
