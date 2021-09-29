using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class RequestCategory
    {
        public RequestCategory()
        {
            StudentRequests = new HashSet<StudentRequest>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public double? Value { get; set; }

        public virtual ICollection<StudentRequest> StudentRequests { get; set; }
    }
}
