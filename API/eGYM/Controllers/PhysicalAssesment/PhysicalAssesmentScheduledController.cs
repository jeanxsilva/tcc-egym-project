using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PhysicalAssesmentScheduledController
    {
        public override async Task PreSavingRoutine(PhysicalAssesmentScheduled entity)
        {
            entity = await this.Service.PreSave(entity);
        }

        public override async Task PostSavingRoutine(PhysicalAssesmentScheduled entity)
        {
        }
    }
}