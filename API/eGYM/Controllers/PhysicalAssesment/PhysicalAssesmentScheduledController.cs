using eGYM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PhysicalAssesmentScheduledController
    {
        [HttpPost]
        //[Authorize(Roles = "PhysicalAssesmentScheduled.C, PhysicalAssesmentScheduled.U")]
        [Authorize]
        [Route("UpdateScheduleStatus")]
        public async Task<dynamic> UpdateScheduleStatus(PhysicalAssesmentScheduled entity)
        {
            try
            {
                this.ReturnBag.HasError = false;

                PhysicalAssesmentScheduled physicalAssesmentScheduled = await this.Service.GetByIdAsync(entity.Id);
                physicalAssesmentScheduled.WasAnswered = entity.WasAnswered;
                physicalAssesmentScheduled.WasCanceled = entity.WasCanceled;

                this.ReturnBag.Result = await this.Service.SaveAsync(physicalAssesmentScheduled);
            }
            catch (Exception exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;
            }

            return this.ReturnBag;
        }
    }
}