using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eGYM.Database.Repositories;
using eGYM.Models;
using eGYM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class ShiftController
    {
        #region GetDataColumns()
        
        protected override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("id",DataTypes.Int,"Id"));
            dataColumns.Add(new DataColumn("description", DataTypes.String, "Descrição"));
            return dataColumns;
        }

        #endregion
    }
}
