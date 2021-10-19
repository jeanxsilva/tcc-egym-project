using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class Payment : IEntityBase 
    {
        public Payment()
        {
            PaymentReversals = new HashSet<PaymentReversal>();
        }

        public int Id { get; set; }
        public bool IsValid { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public int InvoiceId { get; set; }
        public int PaidByUserId { get; set; }
        public int CompanyUnitId { get; set; }
        public int ReceivedByUserId { get; set; }

        public virtual CompanyUnit CompanyUnit { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual User PaidByUser { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual User ReceivedByUser { get; set; }
        public virtual ICollection<PaymentReversal> PaymentReversals { get; set; }
    }
}
