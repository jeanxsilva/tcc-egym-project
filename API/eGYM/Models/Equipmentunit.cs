using System;
using System.Collections.Generic;

#nullable disable

namespace eGYM.Models
{
    public partial class Equipmentunit : IEntityBase 
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public bool IsInGoodCondition { get; set; }
        public string SupplierCode { get; set; }
        public int? LastRevisionId { get; set; }
        public int CompanyUnitId { get; set; }
        public int SupplierUnitId { get; set; }
    }
}
