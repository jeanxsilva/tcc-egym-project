using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PaymentMovementService
    {
        private readonly PaymentReversalService paymentReversalService;
        private readonly PaymentReversalStatusService paymentReversalStatusService;
        private readonly UserService userService;
        private readonly PaymentService paymentService;
        private readonly InvoiceStatusService invoiceStatusService;
        private readonly InvoiceService invoiceService;

        public PaymentMovementService(PaymentMovementRepository repository, PaymentReversalService paymentReversalService, PaymentReversalStatusService paymentReversalStatusService,
            UserService userService, PaymentService paymentService, InvoiceStatusService invoiceStatusService, InvoiceService invoiceService) : this(repository)
        {
            this.paymentReversalService = paymentReversalService;
            this.paymentReversalStatusService = paymentReversalStatusService;
            this.userService = userService;
            this.paymentService = paymentService;
            this.invoiceStatusService = invoiceStatusService;
            this.invoiceService = invoiceService;
        }

        public override async Task PreSavingRoutine(PaymentMovement entity)
        {
            IQueryable<PaymentMovement> queryable = this.Repository.GetQuery();
            List<PaymentMovement> movements = queryable.Where(pm => pm.PaymentReversal.Id == entity.PaymentReversalId && pm.IsCurrent == true).ToList();

            foreach (PaymentMovement movement in movements)
            {
                movement.IsCurrent = false;
            }

            PaymentReversal paymentReversal = await this.paymentReversalService.GetByIdAsync(entity.PaymentReversalId);

            if (entity.PaymentReversalStatusId == (int)PaymentReversalStatusEnum.Deffered)
            {
                paymentReversal.Payment.IsValid = false;

                Invoice invoice = paymentReversal.Payment.Invoice;
                await this.invoiceService.CancelInvoice(invoice);
            }

            if (entity.PaymentReversalStatusId >= (int)PaymentReversalStatusEnum.Deffered)
            {
                paymentReversal.FinishedByUser = await this.userService.ResolveUser();
            }

            paymentReversal.LastModifiedDateTime = DateTime.UtcNow.ToLocalTime();
            paymentReversal.PaymentReversalStatus = await this.paymentReversalStatusService.GetByIdAsync(entity.PaymentReversalStatusId);
            entity.PaymentReversal = paymentReversal;
            entity.PaymentReversalStatus = await this.paymentReversalStatusService.GetByIdAsync(entity.PaymentReversalStatusId);

            entity.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            entity.RegisteredByUser = await this.userService.ResolveUser();
            entity.IsCurrent = true;
        }
    }
}
