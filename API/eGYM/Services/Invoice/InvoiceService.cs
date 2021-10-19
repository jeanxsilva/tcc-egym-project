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

        public async Task<Invoice> GenerateInvoice(List<RegistrationModalityClass> registrationModalityClasses, StudentRegistration student, DateTime referentToDate, bool isByRequest, string note)
        {
            Invoice invoice = new Invoice();
            invoice.ReferentToDate = referentToDate;
            invoice.DueDate = referentToDate.AddDays(3);
            invoice.Student = student;
            invoice.IsByRequest = isByRequest;
            invoice.Note = note;
            invoice.CompanyUnit = await this.companyUnitService.GetByIdAsync(1);
            invoice.InvoiceStatus = await this.invoiceStatusService.GetByIdAsync(0);

            List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();
            foreach (RegistrationModalityClass registrationModalityClass in registrationModalityClasses)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                Modality modality = registrationModalityClass.ModalityClass.Modality;

                invoiceDetail.Description = modality.Description;
                invoiceDetail.Price = modality.Price;
                invoiceDetail.Invoice = invoice;

                invoiceDetails.Add(invoiceDetail);

                invoice.TotalValue += modality.Price;
            }

            invoice.RegistrationModalityClasses = registrationModalityClasses;
            invoice.InvoiceDetails = invoiceDetails;

            return await this.Repository.Create(invoice);
        }

        public async Task<bool> SavePaymentInvoice(Invoice invoice)
        {
            List<RegistrationModalityClass> registrationModalityClasses = invoice.RegistrationModalityClasses.ToList();

            if(registrationModalityClasses.Count > 0)
            {
                foreach(RegistrationModalityClass registrationModalityClass in registrationModalityClasses)
                {
                    registrationModalityClass.IsValid = true;
                }

                await this.registrationModalityClassService.SaveAsync(registrationModalityClasses);
            }

            invoice.InvoiceStatus = await this.invoiceStatusService.GetByIdAsync(10);
            await this.Repository.Update(invoice);
            return false;
        }
    }
}
