using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class ModalityService
    {
        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("id", DataTypes.Int, "Id"));
            dataColumns.Add(new DataColumn("description", DataTypes.String, "Descrição"));
            dataColumns.Add(new DataColumn("price", DataTypes.Currency, "Preço"));
            dataColumns.Add(new DataColumn("daysInWeek", DataTypes.Currency, "Dias na semana"));

            return dataColumns;
        }

        #endregion
    }
}
