using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class PhysicalAssesment : IEntityBase
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int? RegisteredByEmployeeId { get; set; }
        public string StudentGoal { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public int? ScheduledPhysicalAssesmentId { get; set; }
        public int StudentCaracteristicsId { get; set; }
        public int? CompanyUnitId { get; set; }

        public virtual CompanyUnit CompanyUnit { get; set; }
        public virtual Employee RegisteredByEmployee { get; set; }
        public virtual PhysicalAssesmentScheduled ScheduledPhysicalAssesment { get; set; }
        public virtual StudentRegistration Student { get; set; }
        public virtual StudentCaracteristic StudentCaracteristics { get; set; }
    }
}
