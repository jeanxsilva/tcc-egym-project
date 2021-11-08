using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class RequestCategoryLevel : IEntityBase
    {
        public int Id { get; set; }
        public int UserLevelId { get; set; }
        public int RequestCategoryId { get; set; }

        public virtual RequestCategory RequestCategory { get; set; }
        public virtual UserLevel UserLevel { get; set; }
    }
}
