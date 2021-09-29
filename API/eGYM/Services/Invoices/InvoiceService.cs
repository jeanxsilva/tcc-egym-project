using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class InvoiceService : ServiceBase<Invoice, InvoiceRepository>
    {
        public InvoiceService(InvoiceRepository repository)
        {
            this.Repository = repository;
        }
    }
}
