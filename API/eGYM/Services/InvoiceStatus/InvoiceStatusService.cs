using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class InvoiceStatusService : ServiceBase<InvoiceStatus, InvoiceStatusRepository>
    {
        public InvoiceStatusService(InvoiceStatusRepository repository)
        {
            this.Repository = repository;
        }
    }
}
