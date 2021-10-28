using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class ClassCheckInOut : IEntityBase
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool IsTraining { get; set; }
        public int StudentId { get; set; }
        public int? ModalityClassId { get; set; }

        public virtual ModalityClass ModalityClass { get; set; }
        public virtual StudentRegistration Student { get; set; }
    }
}
