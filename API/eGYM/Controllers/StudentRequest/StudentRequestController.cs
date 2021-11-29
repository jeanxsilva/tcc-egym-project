using eGYM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class StudentRequestController
    {
        [HttpPost]
        [Route("FinishRequest")]
        public async Task<dynamic> FinishRequest([FromBody]int requestId, int requestStatusEnum)
        {
            try
            {
                this.ReturnBag.HasError = false;

                StudentRequest studentRequest = await this.Service.GetByIdAsync(requestId);
                bool wasFinished = await this.Service.FinishRequest(studentRequest, (RequestStatusEnum)requestStatusEnum);

                this.ReturnBag.Result = wasFinished;
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
        [Route("CancelRequest")]
        public async Task<dynamic> CancelRequest([FromBody] int requestId)
        {
            try
            {
                this.ReturnBag.HasError = false;
                bool wasRequested = await this.Service.CancelRequest(requestId);

                this.ReturnBag.Result = wasRequested;
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
        [Route("RequestReversalPayment")]
        public async Task<dynamic> RequestReversalPayment(PaymentReversal paymentReversal)
        {
            try
            {
                this.ReturnBag.HasError = false;
                bool wasRequested = await this.Service.RequestReversalPayment(paymentReversal);

                this.ReturnBag.Result = wasRequested;
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
        [Route("RequestChangeTraining")]
        public async Task<dynamic> RequestChangeTraining(StudentRequest studentRequest)
        {
            try
            {
                this.ReturnBag.HasError = false;
                bool wasRequested = await this.Service.RequestChangeTraining(studentRequest);

                this.ReturnBag.Result = wasRequested;
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
        [Route("RequestPhysicalAssesment")]
        public async Task<dynamic> RequestPhysicalAssesment(StudentRequest studentRequest)
        {
            try
            {
                this.ReturnBag.HasError = false;
                bool wasRequested = await this.Service.RequestPhysicalAssesment(studentRequest);

                this.ReturnBag.Result = wasRequested;
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
    }
}