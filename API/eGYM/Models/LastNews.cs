using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class LastNews : IEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public string Options { get; set; }
        public DateTime ExpireDateTime { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public int PublishedByUserId { get; set; }
        public int? CompanyUnitId { get; set; }

        public virtual CompanyUnit CompanyUnit { get; set; }
        public virtual User PublishedByUser { get; set; }
    }
}
