using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class StudentRegistrationController
    {
        #region GetDataColumns()

        protected override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("id", DataTypes.Int, "Id"));
            dataColumns.Add(new DataColumn("user.name", DataTypes.String, "Nome"));
            return dataColumns;
        }

        #endregion
    }
}
