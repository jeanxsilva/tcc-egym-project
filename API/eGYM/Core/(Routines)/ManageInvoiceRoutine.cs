using eGYM.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGYM
{
    public class ManageInvoiceRoutine : IHostedService, IDisposable
    {
        public IServiceProvider serviceProvider { get; set; }
        public Timer timer { get; set; }

        public ManageInvoiceRoutine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            this.timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(this.DoRoutineAsync, null, 0, 86400000);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void DoRoutineAsync(object state)
        {
            try
            {
                using (var scope = this.serviceProvider.CreateScope())
                {
                    InvoiceService invoiceService = scope.ServiceProvider.GetRequiredService<InvoiceService>();
                    RegistrationModalityClassService registrationModalityClassService = scope.ServiceProvider.GetRequiredService<RegistrationModalityClassService>();

                    await registrationModalityClassService.AutoCancelRegistrations();

                    DateTime fiveDaysLater = DateTime.UtcNow.ToLocalTime().AddDays(5).Date;

                    IQueryable<RegistrationModalityClass> rmcQueryable = registrationModalityClassService.Repository.GetQuery();
                    List<RegistrationModalityClass> registrationModalityClasses = rmcQueryable.Where(rmc => rmc.IsValid == true && rmc.ModalityPaymentType.Id == (int)ModalityPaymentTypeEnum.Monthly).ToList();

                    List<int> alreadyGenerated = new List<int>();
                    foreach (RegistrationModalityClass registration in registrationModalityClasses)
                    {
                        DateTime dueDate = new DateTime(fiveDaysLater.Year, fiveDaysLater.Month, registration.DueDay);
                        List<Invoice> invoices = registration.InvoiceDetails.Select(id => id.Invoice).Where(i => i.ReferentToDate.GetValueOrDefault().Month == fiveDaysLater.Month).ToList();

                        if (fiveDaysLater == dueDate && invoices.Count == 0)
                        {
                            StudentRegistration student = registration.StudentRegistration;

                            if (!alreadyGenerated.Contains(student.Id))
                            {
                                List<RegistrationModalityClass> registrationsToGenerateInvoice = registrationModalityClasses.Where(r => r.StudentRegistration.Id == student.Id).ToList();

                                await invoiceService.GenerateInvoice(registrationsToGenerateInvoice, student, dueDate, false, "Fatura referente ao mês " + fiveDaysLater.Month);

                                alreadyGenerated.Add(student.Id);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Salva LOG com descrição do erro: " + e.Message);
            }
        }
    }
}