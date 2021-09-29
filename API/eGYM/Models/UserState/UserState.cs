using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class UserState : IEntityBase
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
