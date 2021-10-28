using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PaymentReversalService
    {
        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("id", DataTypes.Int, "Id"));
            dataColumns.Add(new DataColumn("authorizedByUser.name", DataTypes.String, "Autorizada por"));
            dataColumns.Add(new DataColumn("paymentReversalStatus.description", DataTypes.String, "Status"));
            dataColumns.Add(new DataColumn("lastModifiedDateTime", DataTypes.Date, "Ultima modificação"));

            return dataColumns;
        }
    }
}
