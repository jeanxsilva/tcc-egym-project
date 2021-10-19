using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class PaymentService
    {
        private readonly PaymentRepository repository;
        private readonly UserService userService;
        private readonly PaymentTypeService paymentTypeService;
        private readonly CompanyService companyService;
        private readonly InvoiceService invoiceService;
        private readonly CompanyUnitService companyUnitService;

        public PaymentService(PaymentRepository repository, UserService userService, PaymentTypeService paymentTypeService, CompanyService companyService, InvoiceService invoiceService, CompanyUnitService companyUnitService) : this(repository)
        {
            this.repository = repository;
            this.userService = userService;
            this.paymentTypeService = paymentTypeService;
            this.companyService = companyService;
            this.invoiceService = invoiceService;
            this.companyUnitService = companyUnitService;
        }

        public async Task<Payment> PreSave(Payment payment)
        {
            payment.PaymentDateTime = DateTime.UtcNow.ToLocalTime();
            payment.PaidByUser = await this.userService.GetByIdAsync(payment.PaidByUserId);
            payment.PaymentType = await this.paymentTypeService.GetByIdAsync(payment.PaymentTypeId);
            payment.ReceivedByUser = await this.userService.GetByIdAsync(1);
            payment.CompanyUnit = await this.companyUnitService.GetByIdAsync(1);
            payment.Invoice = await this.invoiceService.GetByIdAsync(payment.InvoiceId);

            return payment;
        }

        public async Task<bool> PostSave(Payment payment)
        {
            await this.invoiceService.SavePaymentInvoice(payment.Invoice);
            return false;
        }
    }
}
