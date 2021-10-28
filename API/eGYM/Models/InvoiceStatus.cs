using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class InvoiceStatus : IEntityBase
    {
        public InvoiceStatus()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
