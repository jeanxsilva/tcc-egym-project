using eGYM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class UserProfileController
    {
        [HttpPost]
        [Authorize(Roles = "UserProfile.U")]
        [Route("ChangeUserPassword")]
        public async Task<dynamic> ChangeUserPassword(int userId, string newPassword)
        {
            try
            {
                this.ReturnBag.HasError = false;
                bool isUpdated = await this.Service.ChangeUserPassword(userId, newPassword);

                if (isUpdated)
                {
                    this.ReturnBag.Message = "Senha alterada com sucesso";
                }
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