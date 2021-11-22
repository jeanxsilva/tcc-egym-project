using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class PaymentReversal : IEntityBase
    {
        public PaymentReversal()
        {
            PaymentMovements = new HashSet<PaymentMovement>();
            StudentRequests = new HashSet<StudentRequest>();
        }

        public int Id { get; set; }
        public string Reason { get; set; }
        public int? AuthorizedByUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? FinishedByUserId { get; set; }
        public int PaymentId { get; set; }
        public int PaymentReversalStatusId { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        public virtual User AuthorizedByUser { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User FinishedByUser { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual PaymentReversalStatus PaymentReversalStatus { get; set; }
        public virtual ICollection<PaymentMovement> PaymentMovements { get; set; }
        public virtual ICollection<StudentRequest> StudentRequests { get; set; }
    }
}
