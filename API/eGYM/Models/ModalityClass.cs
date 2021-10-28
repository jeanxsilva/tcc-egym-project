using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class ModalityClass : IEntityBase
    {
        public ModalityClass()
        {
            ClassCheckInOuts = new HashSet<ClassCheckInOut>();
            RegistrationModalityClasses = new HashSet<RegistrationModalityClass>();
        }

        public int Id { get; set; }
        public int ModalityId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? InstructorId { get; set; }
        public int TotalVacancies { get; set; }
        public int TotalActiveMembers { get; set; }
        public int CompanyUnitId { get; set; }

        public virtual CompanyUnit CompanyUnit { get; set; }
        public virtual User Instructor { get; set; }
        public virtual Modality Modality { get; set; }
        public virtual ICollection<ClassCheckInOut> ClassCheckInOuts { get; set; }
        public virtual ICollection<RegistrationModalityClass> RegistrationModalityClasses { get; set; }
    }
}
