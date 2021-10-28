using eGYM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PhysicalAssesmentController
    {
        [HttpPost]
        [Route("CalculateCaracteristics")]
        public async Task<dynamic> CalculateCaracteristics([FromBody] StudentCaracteristic studentCaracteristic)
        {
            try
            {
                this.ReturnBag.HasError = false;
                StudentCaracteristic calculatedStudentCaracteristic = await this.Service.CalculateStudentCaracteristicsAsync(studentCaracteristic);
                this.ReturnBag.Result = calculatedStudentCaracteristic;
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
