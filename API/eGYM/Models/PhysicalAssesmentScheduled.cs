using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class PhysicalAssesmentScheduled : IEntityBase 
    {
        public PhysicalAssesmentScheduled()
        {
            PhysicalAssesments = new HashSet<PhysicalAssesment>();
        }

        public int Id { get; set; }
        public int StudentRegistrationId { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public bool WasAnswered { get; set; }
        public bool WasCanceled { get; set; }
        public string Note { get; set; }
        public DateTime ScheduledToDate { get; set; }

        public virtual StudentRegistration StudentRegistration { get; set; }
        public virtual ICollection<PhysicalAssesment> PhysicalAssesments { get; set; }
    }
}
