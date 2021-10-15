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
        //[HttpPost]
        //[Authorize(Roles = "User.C, User.U")]
        //[Authorize(Roles = "StudentRegistration.C, StudentRegistration.U")]
        //[Route("SaveStudent")]
        //public async Task<dynamic> SaveStudent(User user)
        //{
        //    try
        //    {
        //        this.ReturnBag.HasError = false;
        //        bool isUpdated = await this.Service.SaveStudent(user);

        //        if (isUpdated)
        //        {
        //            this.ReturnBag.Message = "Aluno salvo com sucesso";
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        this.ReturnBag.HasError = true;
        //        this.ReturnBag.Message = exception.Message;
        //    }

        //    return this.ReturnBag;
        //}
    }
}