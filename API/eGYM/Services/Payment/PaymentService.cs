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


        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("paidByUser.name", DataTypes.String, "Nome do aluno"));
            dataColumns.Add(new DataColumn("paymentType.description", DataTypes.String, "Tipo de pagamento"));
            dataColumns.Add(new DataColumn("invoice.totalValue", DataTypes.Currency, "Valor"));
            dataColumns.Add(new DataColumn("paymentDateTime", DataTypes.Date, "Data de pagamento"));

            return dataColumns;
        }

        #endregion

        public override async Task PreSavingRoutine(Payment payment)
        {
            payment.PaymentDateTime = DateTime.UtcNow.ToLocalTime();
            payment.PaidByUser = await this.userService.GetByIdAsync(payment.PaidByUserId);
            payment.PaymentType = await this.paymentTypeService.GetByIdAsync(payment.PaymentTypeId);
            payment.ReceivedByUser = await this.userService.ResolveUser();
            payment.CompanyUnit = await this.companyUnitService.ResolveCompanyUnit();
            payment.Invoice = await this.invoiceService.GetByIdAsync(payment.InvoiceId);
            payment.IsValid = true;
        }

        public override async Task PostSavingRoutine(Payment payment)
        {
            await this.invoiceService.SavePaymentInvoice(payment.Invoice);
        }
    }
}
