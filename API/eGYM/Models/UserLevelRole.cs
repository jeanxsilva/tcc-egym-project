using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class UserLevelRole : IEntityBase 
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public int UserLevelId { get; set; }

        public virtual UserLevel UserLevel { get; set; }
    }
}
