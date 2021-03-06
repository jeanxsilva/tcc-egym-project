using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class RegistrationModalityClass : IEntityBase
    {
        public RegistrationModalityClass()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int Id { get; set; }
        public int StudentRegistrationId { get; set; }
        public int ModalityClassId { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public bool IsValid { get; set; }
        public int DueDay { get; set; }
        public int ModalityPaymentTypeId { get; set; }

        public virtual ModalityClass ModalityClass { get; set; }
        public virtual ModalityPaymentType ModalityPaymentType { get; set; }
        public virtual StudentRegistration StudentRegistration { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
