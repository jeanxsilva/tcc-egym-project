using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class ExerciseService
    {
        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("id", DataTypes.Int, "Id"));
            dataColumns.Add(new DataColumn("description", DataTypes.String, "Descrição"));
            dataColumns.Add(new DataColumn("exerciseCategory.description", DataTypes.String, "Categoria"));
            return dataColumns;
        }

        #endregion
    }
}