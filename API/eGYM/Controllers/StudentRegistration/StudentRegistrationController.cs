using eGYM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class StudentRegistrationController
    {

        [HttpPost]
        [Authorize(Roles = "User.C, User.U")]
        [Authorize(Roles = "StudentRegistration.C, StudentRegistration.U")]
        [Route("SaveStudent")]
        public async Task<dynamic> SaveStudent(StudentRegistration studentRegistration)
        {
            try
            {
                this.ReturnBag.HasError = false;
                StudentRegistration savedStudent = await this.Service.SaveStudentUser(studentRegistration);

                if (savedStudent != null)
                {
                    this.ReturnBag.Result = savedStudent;
                    this.ReturnBag.Message = "Aluno salvo com sucesso";
                }
            }
            catch (DbUpdateException exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;

                MySqlException sqlException = exception.GetBaseException() as MySqlException;

                if (sqlException != null)
                {
                    int number = sqlException.Number;

                    if (number == 1062)
                    {
                        this.ReturnBag.Message = "Registro duplicado em chave unica! Possivelmente existe outro registro com o mesmo valor.";
                    }
                }
            }
            catch (Exception exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;
            }

            return this.ReturnBag;
        }

        [HttpPost]
        [Authorize(Roles = "User.D")]
        [Authorize(Roles = "StudentRegistration.D")]
        [Route("DeleteStudent")]
        public async Task<dynamic> DeleteStudent(StudentRegistration studentRegistration)
        {
            try
            {
                this.ReturnBag.HasError = false;
                bool wasRemoved = await this.Service.DeleteStudentUser(studentRegistration);

                if (wasRemoved)
                {
                    this.ReturnBag.Message = "Aluno removido com sucesso";
                }
            }
            catch (DbUpdateException exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;

                MySqlException sqlException = exception.GetBaseException() as MySqlException;

                if (sqlException != null)
                {
                    int number = sqlException.Number;

                    if (number == 1451)
                    {
                        this.ReturnBag.Message = "A entidade possui registros dependentes que devem ser excluidos anteriormente.";
                    }
                }
            }
            catch (Exception exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;
            }

            return this.ReturnBag;
        }

        [HttpPost]
        [Authorize(Roles = "StudentRegistration.U")]
        [Authorize(Roles = "RegistrationModalityClass.U")]
        [Route("ChangeRegistration")]
        public async Task<dynamic> ChangeRegistration(StudentRegistration studentRegistration)
        {
            try
            {
                this.ReturnBag.HasError = false;
                this.ReturnBag.Result = await this.Service.ChangeRegistration(studentRegistration);
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
