using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class ModalityClassService
    {
        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("id", DataTypes.Int, "Id"));
            dataColumns.Add(new DataColumn("modality.description", DataTypes.String, "Modalidade"));
            dataColumns.Add(new DataColumn("instructor.user.name", DataTypes.String, "Instrutor"));
            dataColumns.Add(new DataColumn("totalActiveMembers", DataTypes.String, "Membros ativos"));
            dataColumns.Add(new DataColumn("totalVacancies", DataTypes.String, "Total permitido"));

            return dataColumns;
        }

        #endregion
    }
}
