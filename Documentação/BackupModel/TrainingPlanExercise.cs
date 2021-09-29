using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class TrainingPlanExercise
    {
        public int Id { get; set; }
        public int TrainingPlanId { get; set; }
        public int DayOfWeek { get; set; }
        public int Order { get; set; }
        public int ExerciseId { get; set; }
        public sbyte IsCombined { get; set; }
        public int? CombinedExerciseId { get; set; }
        public string Repetition { get; set; }

        public virtual Exercise CombinedExercise { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
    }
}
