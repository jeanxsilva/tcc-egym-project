using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class StudentRegistration : IEntityBase 
    {
        public StudentRegistration()
        {
            ClassCheckInOuts = new HashSet<ClassCheckInOut>();
            Invoices = new HashSet<Invoice>();
            PhysicalAssesmentScheduleds = new HashSet<PhysicalAssesmentScheduled>();
            PhysicalAssesments = new HashSet<PhysicalAssesment>();
            RegistrationModalityClasses = new HashSet<RegistrationModalityClass>();
            StudentCaracteristics = new HashSet<StudentCaracteristic>();
            StudentRequests = new HashSet<StudentRequest>();
            TrainingPlans = new HashSet<TrainingPlan>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public int? ActualTrainingPlanId { get; set; }
        public DateTime RegisterDateTime { get; set; }

        public virtual TrainingPlan ActualTrainingPlan { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ClassCheckInOut> ClassCheckInOuts { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<PhysicalAssesmentScheduled> PhysicalAssesmentScheduleds { get; set; }
        public virtual ICollection<PhysicalAssesment> PhysicalAssesments { get; set; }
        public virtual ICollection<RegistrationModalityClass> RegistrationModalityClasses { get; set; }
        public virtual ICollection<StudentCaracteristic> StudentCaracteristics { get; set; }
        public virtual ICollection<StudentRequest> StudentRequests { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
    }
}
