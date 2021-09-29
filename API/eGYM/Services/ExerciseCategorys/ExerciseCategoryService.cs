using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public class ExerciseCategoryService : ServiceBase<ExerciseCategory, ExerciseCategoryRepository>
    {
        public ExerciseCategoryService(ExerciseCategoryRepository repository)
        {
            this.Repository = repository;
        }
    }
}
