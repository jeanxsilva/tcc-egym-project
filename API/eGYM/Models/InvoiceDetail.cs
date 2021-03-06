using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class InvoiceDetail : IEntityBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int InvoiceId { get; set; }
        public int? RegistrationModalityClassId { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual RegistrationModalityClass RegistrationModalityClass { get; set; }
    }
}
