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
            LastNews = new HashSet<LastNews>();
            PaymentMovements = new HashSet<PaymentMovement>();
            PaymentPaidByUsers = new HashSet<Payment>();
            PaymentReceivedByUsers = new HashSet<Payment>();
            PaymentReversalAuthorizedByUsers = new HashSet<PaymentReversal>();
            PaymentReversalCreatedByUsers = new HashSet<PaymentReversal>();
            PaymentReversalFinishedByUsers = new HashSet<PaymentReversal>();
            StudentRequests = new HashSet<StudentRequest>();
        }

        public int Id { get; set; }
        public string RegisterCode { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? CompanyUnitId { get; set; }
        public bool Genre { get; set; }
        public string AddressCode { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
        public int? AddressNumber { get; set; }
        public string AddressCity { get; set; }

        public virtual CompanyUnit CompanyUnit { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual StudentRegistration StudentRegistration { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<CompanyUnit> CompanyUnits { get; set; }
        public virtual ICollection<LastNews> LastNews { get; set; }
        public virtual ICollection<PaymentMovement> PaymentMovements { get; set; }
        public virtual ICollection<Payment> PaymentPaidByUsers { get; set; }
        public virtual ICollection<Payment> PaymentReceivedByUsers { get; set; }
        public virtual ICollection<PaymentReversal> PaymentReversalAuthorizedByUsers { get; set; }
        public virtual ICollection<PaymentReversal> PaymentReversalCreatedByUsers { get; set; }
        public virtual ICollection<PaymentReversal> PaymentReversalFinishedByUsers { get; set; }
        public virtual ICollection<StudentRequest> StudentRequests { get; set; }
    }
}
