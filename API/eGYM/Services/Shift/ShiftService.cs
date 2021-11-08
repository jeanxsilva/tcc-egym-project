using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class ShiftService
    {

        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            
            dataColumns.Add(new DataColumn("description", DataTypes.String, "Descrição"));
            dataColumns.Add(new DataColumn("startTime", DataTypes.String, "Hora de entrada"));
            dataColumns.Add(new DataColumn("endTime", DataTypes.String, "Hora de saida"));
            return dataColumns;
        }

        #endregion
    }
}
