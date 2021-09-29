using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class ExerciseService : ServiceBase<Exercise, ExerciseRepository>
    {
        public ExerciseService(ExerciseRepository repository)
        {
            this.Repository = repository;
        }
    }
}
