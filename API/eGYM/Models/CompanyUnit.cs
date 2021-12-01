using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class CompanyUnit : IEntityBase
    {
        public CompanyUnit()
        {
            Invoices = new HashSet<Invoice>();
            LastNews = new HashSet<LastNews>();
            ModalityClasses = new HashSet<ModalityClass>();
            Payments = new HashSet<Payment>();
            PhysicalAssesmentScheduleds = new HashSet<PhysicalAssesmentScheduled>();
            PhysicalAssesments = new HashSet<PhysicalAssesment>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public string RegisterCode { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? UserContactId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User UserContact { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<LastNews> LastNews { get; set; }
        public virtual ICollection<ModalityClass> ModalityClasses { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PhysicalAssesmentScheduled> PhysicalAssesmentScheduleds { get; set; }
        public virtual ICollection<PhysicalAssesment> PhysicalAssesments { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
