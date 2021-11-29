using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class InvoiceService
    {
        private readonly InvoiceRepository repository;
        private readonly CompanyUnitService companyUnitService;
        private readonly InvoiceStatusService invoiceStatusService;
        private readonly RegistrationModalityClassService registrationModalityClassService;

        public InvoiceService(InvoiceRepository repository, CompanyUnitService companyUnitService, InvoiceStatusService invoiceStatusService, RegistrationModalityClassService registrationModalityClassService) : this(repository)
        {
            this.repository = repository;
            this.companyUnitService = companyUnitService;
            this.invoiceStatusService = invoiceStatusService;
            this.registrationModalityClassService = registrationModalityClassService;
        }

        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();
            dataColumns.Add(new DataColumn("student.user.name", DataTypes.String, "Nome do aluno"));
            dataColumns.Add(new DataColumn("dueDate", DataTypes.Date, "Data de vencimento"));
            dataColumns.Add(new DataColumn("referentToDate", DataTypes.Date, "Referente a data"));
            dataColumns.Add(new DataColumn("invoiceStatus.description", DataTypes.String, "Status"));
            dataColumns.Add(new DataColumn("totalValue", DataTypes.Currency, "Valor total"));

            return dataColumns;
        }

        #endregion

        public async Task<Invoice> GenerateInvoice(List<RegistrationModalityClass> registrationModalityClasses, StudentRegistration student, DateTime referentToDate, bool isByRequest, string note)
        {
            Invoice invoice = new Invoice();
            invoice.ReferentToDate = referentToDate;
            invoice.DueDate = referentToDate.AddDays(15);
            invoice.Student = student;
            invoice.IsByRequest = isByRequest;
            invoice.Note = note;
            invoice.CompanyUnit = await this.companyUnitService.ResolveCompanyUnit();
            invoice.InvoiceStatus = await this.invoiceStatusService.GetByIdAsync((int)InvoiceStatusEnum.Generated);

            List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();
            foreach (RegistrationModalityClass registrationModalityClass in registrationModalityClasses)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                Modality modality = registrationModalityClass.ModalityClass.Modality;

                invoiceDetail.Description = modality.Description;
                invoiceDetail.Price = modality.Price;
                invoiceDetail.Invoice = invoice;
                invoiceDetail.RegistrationModalityClass = registrationModalityClass;

                invoiceDetails.Add(invoiceDetail);

                invoice.TotalValue += modality.Price;
            }

            invoice.InvoiceDetails = invoiceDetails;

            return await this.Repository.Create(invoice);
        }

        public async Task<bool> GenerateInvoices(List<RegistrationModalityClass> registrationModalityClasses, StudentRegistration student, DateTime referentToDate, bool isByRequest, string note)
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (RegistrationModalityClass registrationModalityClass in registrationModalityClasses)
            {
                Invoice invoice = new Invoice();
                invoice.ReferentToDate = referentToDate;
                invoice.DueDate = referentToDate.AddDays(15);
                invoice.Student = student;
                invoice.IsByRequest = isByRequest;
                invoice.Note = note;
                invoice.CompanyUnit = await this.companyUnitService.ResolveCompanyUnit();
                invoice.InvoiceStatus = await this.invoiceStatusService.GetByIdAsync((int)InvoiceStatusEnum.Generated);

                InvoiceDetail invoiceDetail = new InvoiceDetail();
                Modality modality = registrationModalityClass.ModalityClass.Modality;

                invoiceDetail.Description = modality.Description;
                invoiceDetail.Price = modality.Price;
                invoiceDetail.Invoice = invoice;
                invoiceDetail.RegistrationModalityClass = registrationModalityClass;
                invoice.InvoiceDetails.Add(invoiceDetail);
                invoice.TotalValue += modality.Price;

                invoices.Add(invoice);
            }

            return await this.Repository.InsertOrUpdate(invoices);
        }

        public async Task<bool> SavePaymentInvoice(Invoice invoice)
        {
            List<InvoiceDetail> invoiceDetails = invoice.InvoiceDetails.ToList();

            if (invoiceDetails.Count > 0)
            {
                List<RegistrationModalityClass> registrationModalityClasses = new List<RegistrationModalityClass>();
                foreach (InvoiceDetail invoiceDetail in invoiceDetails)
                {
                    RegistrationModalityClass registrationModalityClass = invoiceDetail.RegistrationModalityClass;

                    if (registrationModalityClass != null)
                    {
                        registrationModalityClass.IsValid = true;
                        registrationModalityClasses.Add(registrationModalityClass);
                    }
                }

                await this.registrationModalityClassService.SaveAsync(registrationModalityClasses);
            }

            invoice.InvoiceStatus = await this.invoiceStatusService.GetByIdAsync((int)InvoiceStatusEnum.Paid);
            await this.Repository.Update(invoice);
            return true;
        }

        public async Task<Invoice> CancelInvoice(Invoice invoice)
        {
            invoice.InvoiceStatus = await this.invoiceStatusService.GetByIdAsync((int)InvoiceStatusEnum.Canceled);
            foreach (InvoiceDetail detail in invoice.InvoiceDetails)
            {
                if (detail.RegistrationModalityClass != null)
                {
                    detail.RegistrationModalityClass.IsValid = false;
                }
            }

            return await this.Repository.Update(invoice);
        }

        public async Task<bool> CancelInvoices(List<Invoice> invoices)
        {
            foreach (Invoice invoice in invoices)
            {
                invoice.InvoiceStatus = await this.invoiceStatusService.GetByIdAsync((int)InvoiceStatusEnum.Canceled);
                foreach (InvoiceDetail detail in invoice.InvoiceDetails)
                {
                    if (detail.RegistrationModalityClass != null)
                    {
                        detail.RegistrationModalityClass.IsValid = false;
                    }
                }
            }

            return await this.Repository.InsertOrUpdate(invoices);
        }

        public async Task<Invoice> GenerateInvoiceByRequest(StudentRequest request, DateTime referentToDate, string note)
        {
            Invoice invoice = new Invoice();
            invoice.ReferentToDate = referentToDate;
            invoice.DueDate = referentToDate.AddDays(3);
            invoice.Student = request.Student;
            invoice.IsByRequest = true;
            invoice.Note = note;
            invoice.CompanyUnit = await this.companyUnitService.ResolveCompanyUnit();
            invoice.InvoiceStatus = await this.invoiceStatusService.GetByIdAsync((int)InvoiceStatusEnum.Generated);
            invoice.TotalValue = (double)request.RequestCategory.Value;

            InvoiceDetail invoiceDetail = new InvoiceDetail();
            invoiceDetail.Description = request.RequestCategory.Description;
            invoiceDetail.Price = (double)request.RequestCategory.Value;
            invoiceDetail.Invoice = invoice;

            invoice.InvoiceDetails = new List<InvoiceDetail>();
            invoice.InvoiceDetails.Add(invoiceDetail);

            return await this.Repository.Create(invoice);
        }

    }
}