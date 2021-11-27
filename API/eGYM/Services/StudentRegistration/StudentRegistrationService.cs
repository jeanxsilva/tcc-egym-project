using eGYM.Core;
using eGYM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class StudentRegistrationService
    {
        private readonly UserLevelRepository userLevelRepository;
        private readonly UserStateRepository userStateRepository;
        private readonly UserProfileService userProfileService;
        private readonly ClaimResolver claimResolver;
        private readonly UserService userService;
        private readonly ModalityClassService modalityClassService;
        private readonly ModalityPaymentTypeService modalityPaymentTypeService;
        private readonly RegistrationModalityClassRepository registrationModalityClassRepository;
        private readonly RegistrationModalityClassService registrationModalityClassService;
        private readonly InvoiceService invoiceService;
        private readonly CompanyUnitService companyUnitService;

        public StudentRegistrationService(StudentRegistrationRepository repository, UserLevelRepository userLevelRepository,
            UserStateRepository userStateRepository, UserProfileService userProfileService,
            ClaimResolver claimResolver,
            UserService userService,
            ModalityClassService modalityClassService,
            ModalityPaymentTypeService modalityPaymentTypeService,
            RegistrationModalityClassRepository registrationModalityClassRepository,
            CompanyUnitService companyUnitService,
            RegistrationModalityClassService registrationModalityClassService, InvoiceService invoiceService) : this(repository)
        {
            this.Repository = repository;
            this.userLevelRepository = userLevelRepository;
            this.userStateRepository = userStateRepository;
            this.userProfileService = userProfileService;
            this.claimResolver = claimResolver;
            this.userService = userService;
            this.modalityClassService = modalityClassService;
            this.modalityPaymentTypeService = modalityPaymentTypeService;
            this.registrationModalityClassRepository = registrationModalityClassRepository;
            this.companyUnitService = companyUnitService;
            this.registrationModalityClassService = registrationModalityClassService;
            this.invoiceService = invoiceService;
        }

        #region GetDataColumns()

        public override List<DataColumn> GetColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();

            dataColumns.Add(new DataColumn("code", DataTypes.String, "Matricula"));
            dataColumns.Add(new DataColumn("user.name", DataTypes.String, "Nome"));

            return dataColumns;
        }

        #endregion

        public StudentRegistration GetStudentByUser(User user)
        {
            DbSet<StudentRegistration> queryable = this.Repository.GetDbSet();
            StudentRegistration student = queryable.FirstOrDefault(st => st.User.Id.Equals(user.Id));

            return student;
        }

        public async Task<StudentRegistration> SaveStudentUser(StudentRegistration studentRegistration)
        {
            User user = studentRegistration.User;
            bool isNew = true;

            using (var context = this.Repository.GetDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    user.CompanyUnit = await this.companyUnitService.ResolveCompanyUnit();
                    User savedUser = await this.userService.SaveAsync(user);

                    if (savedUser != null)
                    {
                        UserProfile userProfile = this.userProfileService.GetUserProfileByUser(savedUser);
                        if (userProfile == null)
                        {
                            UserLevel userLevel = await this.userLevelRepository.GetById((int)UserLevelEnum.Student);
                            UserState userState = await this.userStateRepository.GetById((int)UserStateEnum.Active);

                            userProfile = new UserProfile();
                            userProfile.Login = savedUser.Email;
                            userProfile.User = savedUser;
                            userProfile.UserLevel = userLevel;
                            userProfile.UserState = userState;
                        }

                        userProfile.Password = savedUser.RegisterCode.ToString();

                        UserProfile savedUserProfile = await this.userProfileService.SaveUserProfileAsync(userProfile);
                        if (savedUserProfile == null)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("Não foi possivel salvar o perfil de usuário.");
                        }

                        List<RegistrationModalityClass> registrationModalityClasses = studentRegistration.RegistrationModalityClasses.ToList();
                        if (registrationModalityClasses.Count > 0)
                        {
                            foreach (RegistrationModalityClass registrationModalityClass in registrationModalityClasses)
                            {
                                registrationModalityClass.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
                                registrationModalityClass.StudentRegistration = studentRegistration;
                                registrationModalityClass.IsValid = false;
                                registrationModalityClass.ModalityClass = await this.modalityClassService.GetByIdAsync(registrationModalityClass.ModalityClassId);
                                registrationModalityClass.ModalityPaymentType = await this.modalityPaymentTypeService.GetByIdAsync(registrationModalityClass.ModalityPaymentTypeId);
                            }
                        }

                        studentRegistration.User = savedUser;
                        studentRegistration.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
                        studentRegistration.Code = DateTime.UtcNow.Year.ToString() + DateTime.UtcNow.Month.ToString() + savedUser.RegisterCode;

                        StudentRegistration savedStudentRegistration = await this.Repository.InsertOrUpdate(studentRegistration);
                        if (savedStudentRegistration == null)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("Não foi possivel salvar a matricula do aluno.");
                        }

                        if (isNew && registrationModalityClasses.Count > 0)
                        {
                            Invoice invoice = await this.invoiceService.GenerateInvoice(registrationModalityClasses, studentRegistration, DateTime.UtcNow.ToLocalTime(), false, "Primeira fatura do aluno");
                            if (invoice == null)
                            {
                                dbContextTransaction.Rollback();
                                throw new Exception("Não foi possivel gerar a fatura do aluno.");
                            }
                        }

                        dbContextTransaction.Commit();
                        return savedStudentRegistration;
                    }
                    else
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("Não foi possivel salvar o aluno.");
                    }
                }
            }
        }

        public async Task<bool> DeleteStudentUser(StudentRegistration studentRegistration)
        {
            studentRegistration = await this.Repository.GetById(studentRegistration.Id);

            if (studentRegistration == null)
            {
                throw new Exception("Não foi possivel encontrar o aluno selecionado.");
            }

            using (var context = this.Repository.GetDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    bool wasRemoved = false;

                    User user = studentRegistration.User;
                    UserProfile userProfile = this.userProfileService.GetUserProfileByUser(user);

                    if (userProfile != null)
                    {
                        wasRemoved = await this.userProfileService.DeleteAsync(userProfile);
                    }

                    if (!wasRemoved)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("Não foi possivel remover o perfil de usuário.");
                    }

                    List<RegistrationModalityClass> registrationModalityClasses = studentRegistration.RegistrationModalityClasses.ToList();
                    foreach (RegistrationModalityClass registrationModalityClass in registrationModalityClasses)
                    {
                        wasRemoved = await this.registrationModalityClassService.DeleteAsync(registrationModalityClass);

                        if (!wasRemoved)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("Não foi possivel remover as matriculas ligadas ao aluno.");
                        }
                    }

                    wasRemoved = await this.Repository.RemoveAsync(studentRegistration);

                    if (!wasRemoved)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("Não foi possivel remover a matricula do aluno.");
                    }

                    wasRemoved = await this.userService.DeleteAsync(user);

                    if (!wasRemoved)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("Não foi possivel remover o aluno.");
                    }

                    dbContextTransaction.Commit();
                    return true;
                }
            }
        }
        public async Task<bool> ChangeRegistration(StudentRegistration student)
        {
            List<RegistrationModalityClass> registrations = student.RegistrationModalityClasses.ToList();
            StudentRegistration studentRegistration = await this.Repository.GetById(student.Id);

            List<Invoice> invoices = new List<Invoice>();
            foreach (RegistrationModalityClass registration in registrations)
            {
                registration.ModalityClass = await this.modalityClassService.GetByIdAsync(registration.ModalityClassId);
                registration.ModalityPaymentType = await this.modalityPaymentTypeService.GetByIdAsync(registration.ModalityPaymentTypeId);
                registration.StudentRegistration = studentRegistration;

                if (registration.Id == 0)
                {
                    registration.DueDay = DateTime.UtcNow.Day;
                    registration.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
                }
                else
                {
                    if (!registration.IsValid)
                    {
                        Invoice invoice = await this.registrationModalityClassService.GetLastInvoiceByRegistration(registration);
                        invoices.Add(invoice);
                    }
                }
            }

            if (invoices.Count != 0)
            {
                await this.invoiceService.CancelInvoices(invoices);
            }

            List<RegistrationModalityClass> toInsertRegistrations = registrations.Where(r => r.Id == 0).ToList();
            if (toInsertRegistrations.Count != 0)
            {
                await this.invoiceService.GenerateInvoice(toInsertRegistrations, studentRegistration, DateTime.UtcNow.ToLocalTime(), false, "Primeira fatura da modalidade");
            }

            return await this.registrationModalityClassService.SaveAsync(registrations);
        }
    }
}