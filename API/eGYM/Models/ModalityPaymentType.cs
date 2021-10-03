using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class ModalityPaymentType : IEntityBase 
    {
        public ModalityPaymentType()
        {
            RegistrationModalityClasses = new HashSet<RegistrationModalityClass>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RegistrationModalityClass> RegistrationModalityClasses { get; set; }
    }
}
