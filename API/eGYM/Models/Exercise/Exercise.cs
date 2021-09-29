using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class Exercise : IEntityBase
    {
        public Exercise()
        {
            TrainingPlanExerciseCombinedExercises = new HashSet<TrainingPlanExercise>();
            TrainingPlanExerciseExercises = new HashSet<TrainingPlanExercise>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int ExerciseCategoryId { get; set; }

        public virtual ExerciseCategory ExerciseCategory { get; set; }
        public virtual ICollection<TrainingPlanExercise> TrainingPlanExerciseCombinedExercises { get; set; }
        public virtual ICollection<TrainingPlanExercise> TrainingPlanExerciseExercises { get; set; }
    }
}
