using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class PhysicalAssesment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int RegisteredByEmployeeId { get; set; }
        public string StudentWeight { get; set; }
        public string StudentHeight { get; set; }
        public string StudentGoal { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public int ScheduledPhysicalAssesmentId { get; set; }

        public virtual Employee RegisteredByEmployee { get; set; }
        public virtual PhysicalAssesmentScheduled ScheduledPhysicalAssesment { get; set; }
        public virtual StudentRegistration Student { get; set; }
    }
}
