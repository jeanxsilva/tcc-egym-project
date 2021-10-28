using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public class DataColumn
    {
        public string PropertyName { get; set; }
        public string LabelDescription { get; set; }
        public DataTypes DataType { get; set; }

        public DataColumn(string name, DataTypes type, string description)
        {
            this.PropertyName = name;
            this.DataType = type;
            this.LabelDescription = description;
        }
    }
}
