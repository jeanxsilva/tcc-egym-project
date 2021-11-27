using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class RegistrationModalityClassService
    {
        private readonly InvoiceRepository invoiceRepository;
        private readonly InvoiceStatusRepository invoiceStatusRepository;

        public RegistrationModalityClassService(RegistrationModalityClassRepository repository, InvoiceRepository invoiceRepository, InvoiceStatusRepository invoiceStatusRepository) : this(repository)
        {
            this.invoiceRepository = invoiceRepository;
            this.invoiceStatusRepository = invoiceStatusRepository;
        }
        public async Task<Invoice> GetLastInvoiceByRegistration(RegistrationModalityClass registration)
        {
            IQueryable<RegistrationModalityClass> queryable = this.Repository.GetQuery();
            List<InvoiceDetail> invoiceDetails = queryable.SelectMany(r => r.InvoiceDetails).ToList();
            InvoiceDetail invoiceDetail = invoiceDetails.OrderByDescending(d => d.Id).FirstOrDefault();

            if (invoiceDetail != null)
            {
                return invoiceDetail.Invoice;
            }

            return null;
        }
        public async Task<bool> AutoCancelRegistrations()
        {
            IQueryable<Invoice> invoiceQueryable = this.invoiceRepository.GetQuery();
            DateTime dateNow = DateTime.UtcNow.ToLocalTime().Date;

            List<Invoice> unpaidInvoices = invoiceQueryable.Where(i => (i.InvoiceStatus.Id == (int)InvoiceStatusEnum.Canceled || i.InvoiceStatus.Id == (int)InvoiceStatusEnum.Generated)
                && (i.DueDate.Date == dateNow)).ToList();
            List<InvoiceDetail> invoiceDetails = unpaidInvoices.SelectMany(i => i.InvoiceDetails).Where(d => d.RegistrationModalityClass != null && d.RegistrationModalityClass.IsValid == true).ToList();

            List<RegistrationModalityClass> toCancelRegistrations = new List<RegistrationModalityClass>();
            foreach (InvoiceDetail invoiceDetail in invoiceDetails)
            {
                if (invoiceDetail.RegistrationModalityClass != null)
                {
                    RegistrationModalityClass toCancelRegistration = invoiceDetail.RegistrationModalityClass;
                    toCancelRegistration.IsValid = false;

                    toCancelRegistrations.Add(toCancelRegistration);
                }
            }

            if (toCancelRegistrations.Count != 0)
            {
                await this.Repository.InsertOrUpdate(toCancelRegistrations);
            }

            unpaidInvoices = unpaidInvoices.Where(i => i.InvoiceStatus.Id == (int)InvoiceStatusEnum.Generated).ToList();
            if (unpaidInvoices.Count != 0)
            {
                foreach (Invoice invoice in unpaidInvoices)
                {
                    invoice.InvoiceStatus = await this.invoiceStatusRepository.GetById((int)InvoiceStatusEnum.Canceled);
                }

                await this.invoiceRepository.InsertOrUpdate(unpaidInvoices);
            }

            return true;
        }
    }
}