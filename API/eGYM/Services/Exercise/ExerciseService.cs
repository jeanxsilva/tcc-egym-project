using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class ExerciseService
    {
        private readonly ExerciseCategoryService exerciseCategoryService;

        public ExerciseService(ExerciseRepository repository, ExerciseCategoryService exerciseCategoryService) : this(repository)
        {
            this.exerciseCategoryService = exerciseCategoryService;
        }

        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            
            dataColumns.Add(new DataColumn("description", DataTypes.String, "Descrição"));
            dataColumns.Add(new DataColumn("exerciseCategory.description", DataTypes.String, "Categoria"));
            return dataColumns;
        }

        #endregion

        public override async Task PreSavingRoutine(Exercise entity)
        {
            entity.ExerciseCategory = await this.exerciseCategoryService.GetByIdAsync(entity.ExerciseCategoryId);
        }
    }
}