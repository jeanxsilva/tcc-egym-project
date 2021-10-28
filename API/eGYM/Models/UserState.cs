using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class UserState : IEntityBase
    {
        public UserState()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
