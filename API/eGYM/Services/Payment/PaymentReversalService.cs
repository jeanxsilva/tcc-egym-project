using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PaymentReversalService
    {
        private readonly PaymentService paymentService;
        private readonly PaymentReversalStatusService paymentReversalStatusService;
        private readonly UserService userService;

        public PaymentReversalService(PaymentReversalRepository repository, PaymentService paymentService, PaymentReversalStatusService paymentReversalStatusService, UserService userService) : this(repository)
        {
            this.paymentService = paymentService;
            this.paymentReversalStatusService = paymentReversalStatusService;
            this.userService = userService;
        }
        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            
            dataColumns.Add(new DataColumn("authorizedByUser.name", DataTypes.String, "Autorizada por"));
            dataColumns.Add(new DataColumn("paymentReversalStatus.description", DataTypes.String, "Status"));
            dataColumns.Add(new DataColumn("lastModifiedDateTime", DataTypes.Date, "Ultima modificação"));

            return dataColumns;
        }

        public override async Task PreSavingRoutine(PaymentReversal entity)
        {
            entity.Payment = await this.paymentService.GetByIdAsync(entity.PaymentId);
            entity.PaymentReversalStatus = await this.paymentReversalStatusService.GetByIdAsync(entity.PaymentReversalStatusId);
            entity.LastModifiedDateTime = DateTime.UtcNow.ToLocalTime();
            entity.CreatedByUser = await this.userService.ResolveUser();

            if (entity.AuthorizedByUserId != null && entity.AuthorizedByUserId != 0)
            {
                entity.AuthorizedByUser = await this.userService.GetByIdAsync((int)entity.AuthorizedByUserId);
            }

            PaymentMovement paymentMovement = new PaymentMovement();
            paymentMovement.IsCurrent = true;
            paymentMovement.PaymentReversal = entity;
            paymentMovement.PaymentReversalStatus = await this.paymentReversalStatusService.GetByIdAsync(entity.PaymentReversalStatusId);
            paymentMovement.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            paymentMovement.RegisteredByUser = await this.userService.ResolveUser();

            entity.PaymentMovements.Add(paymentMovement);
        }
    }
}
