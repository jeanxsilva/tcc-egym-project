using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class Employee : IEntityBase
    {
        public Employee()
        {
            ModalityClasses = new HashSet<ModalityClass>();
            PhysicalAssesments = new HashSet<PhysicalAssesment>();
            ShiftBooks = new HashSet<ShiftBook>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }

        public virtual Shift Shift { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ModalityClass> ModalityClasses { get; set; }
        public virtual ICollection<PhysicalAssesment> PhysicalAssesments { get; set; }
        public virtual ICollection<ShiftBook> ShiftBooks { get; set; }
    }
}
