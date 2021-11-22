using eGYM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PaymentReversalController
    {
        [HttpPost]
        [Authorize]
        [Route("CancelReversal")]
        public async Task<dynamic> CancelReversal(int reversalId)
        {
            try
            {
                this.ReturnBag.HasError = false;

                PaymentReversal paymentReversal = await this.Service.GetByIdAsync(reversalId);

                this.ReturnBag.Result = await this.Service.CancelReversal(paymentReversal);
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
