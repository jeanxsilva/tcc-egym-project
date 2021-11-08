using eGYM.Models;
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
            
            dataColumns.Add(new DataColumn("description", DataTypes.String, "Descrição"));
            dataColumns.Add(new DataColumn("price", DataTypes.Currency, "Preço"));
            dataColumns.Add(new DataColumn("daysInWeek", DataTypes.Int, "Dias na semana"));

            return dataColumns;
        }

        #endregion

        public override Task PreSavingRoutine(Modality entity)
        {
            if (entity.DaysInWeek > 7 || entity.DaysInWeek < 1)
            {
                throw new Exception("Não é possivel inserir esta quantidade de dias.");
            }

            return Task.CompletedTask;
        }
    }
}