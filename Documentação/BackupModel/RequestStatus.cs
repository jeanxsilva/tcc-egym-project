using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            StudentRequests = new HashSet<StudentRequest>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<StudentRequest> StudentRequests { get; set; }
    }
}
