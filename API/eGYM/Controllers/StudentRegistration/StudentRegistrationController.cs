using eGYM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class StudentRegistrationController
    {
        #region GetDataColumns()

        protected override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("id", DataTypes.Int, "Id"));
            dataColumns.Add(new DataColumn("code", DataTypes.String, "Matricula"));
            dataColumns.Add(new DataColumn("user.name", DataTypes.String, "Nome"));
            return dataColumns;
        }

        #endregion

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
            catch (Exception exception)
            {
                this.ReturnBag.HasError = true;
                this.ReturnBag.Message = exception.Message;
            }

            return this.ReturnBag;
        }

        public override async Task PreSavingRoutine(StudentRegistration entity)
        {
            // Do something
        }
    }
}
