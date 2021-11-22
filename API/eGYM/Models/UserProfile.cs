using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace eGYM.Models
{
    public partial class UserProfile : IEntityBase
    {
        public int Id { get; set; }
        public string Login { get; set; }

        [NotMapped]
        public string Password { get; set; }
        public string PasswordEncrypted { get; set; }
        public int UserId { get; set; }
        public int UserLevelId { get; set; }
        public int UserStateId { get; set; }

        public virtual User User { get; set; }
        public virtual UserLevel UserLevel { get; set; }
        public virtual UserState UserState { get; set; }
    }
}
