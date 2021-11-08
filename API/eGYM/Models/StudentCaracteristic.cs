using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class StudentCaracteristic : IEntityBase
    {
        public StudentCaracteristic()
        {
            PhysicalAssesments = new HashSet<PhysicalAssesment>();
        }

        public int Id { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double? Triceps { get; set; }
        public double? Chest { get; set; }
        public double? Subaxillary { get; set; }
        public double? Subscapular { get; set; }
        public double? Abdominal { get; set; }
        public double? Suprailiac { get; set; }
        public double? Thigh { get; set; }
        public double? LeanMass { get; set; }
        public double? FatMass { get; set; }
        public double? FatPercentage { get; set; }
        public double BodyMassIndex { get; set; }
        public int AgeAtMoment { get; set; }
        public int StudentRegistrationId { get; set; }
        public double BasalMetabolicRate { get; set; }
        public double? BodyDensity { get; set; }

        public virtual StudentRegistration StudentRegistration { get; set; }
        public virtual ICollection<PhysicalAssesment> PhysicalAssesments { get; set; }
    }
}
