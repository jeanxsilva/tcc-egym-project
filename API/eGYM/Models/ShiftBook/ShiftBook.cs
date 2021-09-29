using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class ShiftBook : IEntityBase
    {
        public int Id { get; set; }
        public DateTime EntryDateTime { get; set; }
        public DateTime? ExitDateTime { get; set; }
        public DateTime ReferentToDate { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
