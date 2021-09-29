using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class TrainingPlanService : ServiceBase<TrainingPlan, TrainingPlanRepository>
    {
        public TrainingPlanService(TrainingPlanRepository repository)
        {
            this.Repository = repository;
        }
    }
}
