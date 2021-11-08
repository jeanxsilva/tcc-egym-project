using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class TrainingPlanService
    {
        private readonly StudentRegistrationService studentRegistrationService;
        private readonly TrainingPlanExerciseService trainingPlanExerciseService;
        private readonly ExerciseService exerciseService;

        public TrainingPlanService(TrainingPlanRepository repository, StudentRegistrationService studentRegistrationService, TrainingPlanExerciseService trainingPlanExerciseService, ExerciseService exerciseService) : this(repository)
        {
            this.studentRegistrationService = studentRegistrationService;
            this.trainingPlanExerciseService = trainingPlanExerciseService;
            this.exerciseService = exerciseService;
        }

        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("specificToStudent.user.name", DataTypes.String, "Nome do aluno"));
            dataColumns.Add(new DataColumn("description", DataTypes.String, "Descrição"));
            dataColumns.Add(new DataColumn("registerDateTime", DataTypes.Date, "Data de registro"));
            dataColumns.Add(new DataColumn("isActive", DataTypes.Boolean, "É o atual?"));

            return dataColumns;
        }

        #endregion

        public override async Task PreSavingRoutine(TrainingPlan entity)
        {
            entity.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            entity.SpecificToStudent = await this.studentRegistrationService.GetByIdAsync((int)entity.SpecificToStudentId);

            List<TrainingPlanExercise> trainingPlanExercises = new List<TrainingPlanExercise>();
            foreach (TrainingPlanExercise exercise in entity.TrainingPlanExercises)
            {
                exercise.Exercise = await this.exerciseService.GetByIdAsync(exercise.ExerciseId);

                if (exercise.CombinedExerciseId != null)
                {
                    exercise.CombinedExercise = await this.exerciseService.GetByIdAsync((int)exercise.CombinedExerciseId);
                }

                trainingPlanExercises.Add(exercise);
            }

            IQueryable<TrainingPlan> queryable = this.Repository.GetQuery();

            List<TrainingPlan> trainingPlans = queryable.Where(tp => tp.SpecificToStudent.Id == entity.SpecificToStudent.Id).ToList();
            foreach (TrainingPlan trainingPlan in trainingPlans)
            {
                trainingPlan.IsActive = false;
            }

            await this.Repository.InsertOrUpdate(trainingPlans);

            entity.IsActive = true;
            entity.SpecificToStudent.ActualTrainingPlan = entity;
            entity.TrainingPlanExercises = trainingPlanExercises;
        }
    }
}