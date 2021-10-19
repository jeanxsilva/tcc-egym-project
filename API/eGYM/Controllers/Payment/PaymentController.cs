using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PaymentController
    {
        public override async Task PreSavingRoutine(Payment entity)
        {
            entity = await this.Service.PreSave(entity);
        }
        public override async Task PostSavingRoutine(Payment entity)
        {
            await this.Service.PostSave(entity);
        }
    }
}