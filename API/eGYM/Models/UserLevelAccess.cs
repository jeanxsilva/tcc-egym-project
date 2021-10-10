using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class UserLevelAccess : IEntityBase
    {
        public UserLevelAccess()
        {
            InverseParent = new HashSet<UserLevelAccess>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string IconKey { get; set; }
        public int UserLevelId { get; set; }
        public bool HasChild { get; set; }
        public int? ParentId { get; set; }

        public virtual UserLevelAccess Parent { get; set; }
        public virtual UserLevel UserLevel { get; set; }
        public virtual ICollection<UserLevelAccess> InverseParent { get; set; }
    }
}
