using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class TrainingPlanExerciseService : ServiceBase<TrainingPlanExercise, TrainingPlanExerciseRepository>
    {
        public TrainingPlanExerciseService(TrainingPlanExerciseRepository repository)
        {
            this.Repository = repository;
        }
    }
}
