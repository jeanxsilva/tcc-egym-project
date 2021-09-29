using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
            Payments = new HashSet<Payment>();
            StudentRequests = new HashSet<StudentRequest>();
        }

        public int Id { get; set; }
        public double TotalValue { get; set; }
        public DateTime? ReferentToDate { get; set; }
        public DateTime DueDate { get; set; }
        public int InvoiceStatusId { get; set; }
        public int CompanyUnitId { get; set; }
        public int StudentId { get; set; }
        public string Note { get; set; }
        public bool IsByRequest { get; set; }

        public virtual CompanyUnit CompanyUnit { get; set; }
        public virtual InvoiceStatus InvoiceStatus { get; set; }
        public virtual StudentRegistration Student { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<StudentRequest> StudentRequests { get; set; }
    }
}
