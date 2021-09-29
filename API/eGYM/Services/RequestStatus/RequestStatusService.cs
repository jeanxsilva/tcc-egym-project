using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class RequestStatusService : ServiceBase<RequestStatus, RequestStatusRepository>
    {
        public RequestStatusService(RequestStatusRepository repository)
        {
            this.Repository = repository;
        }
    }
}
