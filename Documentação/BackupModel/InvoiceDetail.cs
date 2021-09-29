using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class InvoiceDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
