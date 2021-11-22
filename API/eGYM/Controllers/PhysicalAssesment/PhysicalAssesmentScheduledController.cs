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

                PhysicalAssesmentScheduled saved = await this.Service.SaveAsync(physicalAssesmentScheduled);

                this.ReturnBag.Result = saved != null;
            }
            catch (Exception exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;
            }

            return this.ReturnBag;
        }

        [HttpPost]
        [Authorize]
        [Route("CancelSchedule")]
        public async Task<dynamic> CancelSchedule(int scheduleId)
        {
            try
            {
                this.ReturnBag.HasError = false;

                PhysicalAssesmentScheduled physicalAssesmentScheduled = await this.Service.GetByIdAsync(scheduleId);

                this.ReturnBag.Result = await this.Service.CancelSchedule(physicalAssesmentScheduled);
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