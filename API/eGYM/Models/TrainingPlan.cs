using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class TrainingPlan : IEntityBase
    {
        public TrainingPlan()
        {
            StudentRegistrations = new HashSet<StudentRegistration>();
            TrainingPlanExercises = new HashSet<TrainingPlanExercise>();
        }

        public int Id { get; set; }
        public int? SpecificToStudentId { get; set; }
        public string Description { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public string Note { get; set; }

        public virtual StudentRegistration SpecificToStudent { get; set; }
        public virtual ICollection<StudentRegistration> StudentRegistrations { get; set; }
        public virtual ICollection<TrainingPlanExercise> TrainingPlanExercises { get; set; }
    }
}