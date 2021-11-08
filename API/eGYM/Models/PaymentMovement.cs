using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class PaymentMovement : IEntityBase
    {
        public int Id { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public bool IsCurrent { get; set; }
        public int PaymentReversalStatusId { get; set; }
        public int RegisteredByUserId { get; set; }
        public string Note { get; set; }
        public int PaymentReversalId { get; set; }

        public virtual PaymentReversal PaymentReversal { get; set; }
        public virtual PaymentReversalStatus PaymentReversalStatus { get; set; }
        public virtual User RegisteredByUser { get; set; }
    }
}
