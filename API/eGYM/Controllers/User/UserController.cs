using eGYM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class UserController
    {
        [HttpPost]
        [Authorize(Roles = "User.C")]
        [Authorize(Roles = "StudentRegistration.C")]
        [Route("InsertNewStudent")]
        public async Task<dynamic> InsertNewStudent(User entity)
        {
            try
            {
                this.ReturnBag.HasError = false;
                bool isInserted = await this.Service.InsertNewStudentAsync(entity);
                
                if (isInserted)
                {
                    this.ReturnBag.Message = "Aluno inserido com sucesso";
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
