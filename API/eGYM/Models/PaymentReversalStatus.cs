using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class PaymentReversalStatus : IEntityBase 
    {
        public PaymentReversalStatus()
        {
            PaymentMovements = new HashSet<PaymentMovement>();
            PaymentReversals = new HashSet<PaymentReversal>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PaymentMovement> PaymentMovements { get; set; }
        public virtual ICollection<PaymentReversal> PaymentReversals { get; set; }
    }
}
