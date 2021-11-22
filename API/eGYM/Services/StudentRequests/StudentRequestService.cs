using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class StudentRequestService
    {
        private readonly StudentRequestRepository repository;
        private readonly StudentRegistrationRepository studentRegistrationRepository;
        private readonly UserService userService;
        private readonly EmployeeRepository employeeRepository;
        private readonly PhysicalAssesmentScheduledService physicalAssesmentScheduledService;
        private readonly RequestStatusRepository requestStatusRepository;
        private readonly RequestCategoryRepository requestCategoryRepository;
        private readonly InvoiceStatusRepository invoiceStatusRepository;
        private readonly InvoiceService invoiceService;
        private readonly PaymentReversalRepository paymentReversalRepository;
        private readonly PaymentReversalStatusRepository paymentReversalStatusRepository;
        private readonly PaymentReversalService paymentReversalService;

        public StudentRequestService(StudentRequestRepository repository, StudentRegistrationRepository studentRegistrationRepository, UserService userService,
            EmployeeRepository employeeRepository,
            PhysicalAssesmentScheduledService physicalAssesmentScheduledService,
            RequestStatusRepository requestStatusRepository, RequestCategoryRepository requestCategoryRepository, InvoiceStatusRepository invoiceStatusRepository,
            InvoiceService invoiceService, PaymentReversalRepository paymentReversalRepository, PaymentReversalStatusRepository paymentReversalStatusRepository,
            PaymentReversalService paymentReversalService) : this(repository)
        {
            this.repository = repository;
            this.studentRegistrationRepository = studentRegistrationRepository;
            this.userService = userService;
            this.employeeRepository = employeeRepository;
            this.physicalAssesmentScheduledService = physicalAssesmentScheduledService;
            this.requestStatusRepository = requestStatusRepository;
            this.requestCategoryRepository = requestCategoryRepository;
            this.invoiceStatusRepository = invoiceStatusRepository;
            this.invoiceService = invoiceService;
            this.paymentReversalRepository = paymentReversalRepository;
            this.paymentReversalStatusRepository = paymentReversalStatusRepository;
            this.paymentReversalService = paymentReversalService;
        }

        public override List<DataColumn> GetColumns()
        {
            return base.GetColumns();
        }

        public override Task PreSavingRoutine(StudentRequest entity)
        {
            return base.PreSavingRoutine(entity);
        }

        public override Task PostSavingRoutine(StudentRequest entity)
        {
            return base.PostSavingRoutine(entity);
        }

        public override Task PreDeleteRoutine(StudentRequest entity)
        {
            return base.PreDeleteRoutine(entity);
        }

        public override Task PostDeleteRoutine(StudentRequest entity)
        {
            return base.PostDeleteRoutine(entity);
        }

        public override Task PreUpdateRoutine(StudentRequest entity)
        {
            return base.PreUpdateRoutine(entity);
        }

        public override Task PostUpdateRoutine(StudentRequest entity)
        {
            return base.PostUpdateRoutine(entity);
        }

        public async Task<bool> RequestPhysicalAssesment(StudentRequest studentRequest)
        {
            PhysicalAssesmentScheduled physicalAssesmentScheduled = studentRequest.PhysicalAssesmentScheduled;
            physicalAssesmentScheduled.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            physicalAssesmentScheduled.StudentRegistration = await this.studentRegistrationRepository.GetById(physicalAssesmentScheduled.StudentRegistrationId);
            physicalAssesmentScheduled.WasAnswered = false;
            physicalAssesmentScheduled.WasCanceled = false;

            PhysicalAssesmentScheduled savedScheduled = await this.physicalAssesmentScheduledService.SaveAsync(physicalAssesmentScheduled);

            studentRequest.PhysicalAssesmentScheduled = savedScheduled;
            studentRequest.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            studentRequest.Student = savedScheduled.StudentRegistration;
            studentRequest.RequestStatus = await this.requestStatusRepository.GetById((int)RequestStatusEnum.Opened);
            studentRequest.RequestCategory = await this.requestCategoryRepository.GetById(1);

            StudentRequest savedRequest = await this.Repository.InsertOrUpdate(studentRequest);

            await this.invoiceService.GenerateInvoiceByRequest(savedRequest, DateTime.UtcNow.ToLocalTime(), "");

            if (savedRequest != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> RequestChangeTraining(StudentRequest studentRequest)
        {
            studentRequest.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            studentRequest.RequestStatus = await this.requestStatusRepository.GetById((int)RequestStatusEnum.Opened);
            studentRequest.Student = await this.studentRegistrationRepository.GetById(studentRequest.StudentId);
            studentRequest.RequestCategory = await this.requestCategoryRepository.GetById(2);

            StudentRequest savedRequest = await this.Repository.InsertOrUpdate(studentRequest);

            if (savedRequest != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> RequestReversalPayment(PaymentReversal paymentReversal)
        {
            await this.paymentReversalService.PreSavingRoutine(paymentReversal);
            await this.paymentReversalService.SaveAsync(paymentReversal);

            StudentRequest studentRequest = new StudentRequest();
            studentRequest.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
            studentRequest.RequestStatus = await this.requestStatusRepository.GetById((int)RequestStatusEnum.Opened);

            User user = await this.userService.GetByIdAsync(paymentReversal.CreatedByUser.Id);
            studentRequest.Student = user.StudentRegistration;
            studentRequest.RequestCategory = await this.requestCategoryRepository.GetById(2);
            studentRequest.PaymentReversal = paymentReversal;

            StudentRequest savedRequest = await this.Repository.InsertOrUpdate(studentRequest);

            if (savedRequest != null)
            {
                return true;
            }

            return false;
        }

        public async Task<dynamic> CancelRequest(int requestId)
        {
            StudentRequest studentRequest = await this.Repository.GetById(requestId);
            studentRequest.RequestStatus = await this.requestStatusRepository.GetById((int)RequestStatusEnum.Canceled);
            studentRequest.WasCanceled = true;
            studentRequest.ClosedByUser = await this.userService.ResolveUser();

            PhysicalAssesmentScheduled physicalAssesmentScheduled = studentRequest.PhysicalAssesmentScheduled;
            if (physicalAssesmentScheduled != null)
            {
                physicalAssesmentScheduled.WasCanceled = true;
                await this.physicalAssesmentScheduledService.SaveAsync(physicalAssesmentScheduled);
            }

            Invoice invoice = studentRequest.Invoice;
            if (invoice != null)
            {
                invoice.InvoiceStatus = await this.invoiceStatusRepository.GetById((int)InvoiceStatusEnum.Canceled);
                await this.invoiceService.SaveAsync(invoice);
            }

            PaymentReversal paymentReversal = studentRequest.PaymentReversal;
            if (paymentReversal != null)
            {
                paymentReversal.PaymentReversalStatus = await this.paymentReversalStatusRepository.GetById(paymentReversal.PaymentReversalStatusId);
                await paymentReversalRepository.InsertOrUpdate(paymentReversal);
            }

            return true;
        }
    }
}