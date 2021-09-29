using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class Shift
    {
        public Shift()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
