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
        public int RegisteredByEmployeeId { get; set; }

        public virtual PaymentReversalStatus PaymentReversalStatus { get; set; }
        public virtual Employee RegisteredByEmployee { get; set; }
    }
}
