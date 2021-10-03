using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class StudentRequest : IEntityBase 
    {
        public int Id { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public int StudentId { get; set; }
        public int RequestStatusId { get; set; }
        public int RequestCategoryId { get; set; }
        public string Note { get; set; }
        public bool IsPaid { get; set; }
        public int? InvoiceId { get; set; }
        public string Attachment { get; set; }
        public int? ReferToChangeModalityClassId { get; set; }
        public int? ClosedByUserId { get; set; }

        public virtual User ClosedByUser { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual RegistrationModalityClass ReferToChangeModalityClass { get; set; }
        public virtual RequestCategory RequestCategory { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
        public virtual StudentRegistration Student { get; set; }
    }
}
