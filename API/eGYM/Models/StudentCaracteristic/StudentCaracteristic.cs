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
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Triceps { get; set; }
        public string Chest { get; set; }
        public string Subaxillary { get; set; }
        public string Subscapular { get; set; }
        public string Abdominal { get; set; }
        public string Suprailiac { get; set; }
        public string Thigh { get; set; }
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
