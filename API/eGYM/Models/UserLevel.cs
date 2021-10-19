using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class UserLevel : IEntityBase 
    {
        public UserLevel()
        {
            UserLevelAccesses = new HashSet<UserLevelAccess>();
            UserLevelRoles = new HashSet<UserLevelRole>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string RoleCode { get; set; }

        public virtual ICollection<UserLevelAccess> UserLevelAccesses { get; set; }
        public virtual ICollection<UserLevelRole> UserLevelRoles { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
