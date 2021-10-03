using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class User : IEntityBase 
    {
        public User()
        {
            CompanyUnits = new HashSet<CompanyUnit>();
            Employees = new HashSet<Employee>();
            LastNews = new HashSet<LastNews>();
            ModalityClasses = new HashSet<ModalityClass>();
            PaymentPaidByUsers = new HashSet<Payment>();
            PaymentReceivedByUsers = new HashSet<Payment>();
            PaymentReversalAuthorizedByUsers = new HashSet<PaymentReversal>();
            PaymentReversalCreatedByUsers = new HashSet<PaymentReversal>();
            PaymentReversalFinishedByUsers = new HashSet<PaymentReversal>();
            StudentRequests = new HashSet<StudentRequest>();
        }

        public int Id { get; set; }
        public string RegisterCode { get; set; }
        public string Description { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? CompanyUnitId { get; set; }
        public bool Genre { get; set; }

        public virtual CompanyUnit CompanyUnit { get; set; }
        public virtual StudentRegistration StudentRegistration { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<CompanyUnit> CompanyUnits { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<LastNews> LastNews { get; set; }
        public virtual ICollection<ModalityClass> ModalityClasses { get; set; }
        public virtual ICollection<Payment> PaymentPaidByUsers { get; set; }
        public virtual ICollection<Payment> PaymentReceivedByUsers { get; set; }
        public virtual ICollection<PaymentReversal> PaymentReversalAuthorizedByUsers { get; set; }
        public virtual ICollection<PaymentReversal> PaymentReversalCreatedByUsers { get; set; }
        public virtual ICollection<PaymentReversal> PaymentReversalFinishedByUsers { get; set; }
        public virtual ICollection<StudentRequest> StudentRequests { get; set; }
    }
}
