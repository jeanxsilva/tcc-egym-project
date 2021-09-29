using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class Company : IEntityBase
    {
        public Company()
        {
            CompanyUnits = new HashSet<CompanyUnit>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string RegisterCode { get; set; }

        public virtual ICollection<CompanyUnit> CompanyUnits { get; set; }
    }
}
