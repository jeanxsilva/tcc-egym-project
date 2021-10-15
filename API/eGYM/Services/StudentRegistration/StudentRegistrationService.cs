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
        private readonly UserService userService;
        private readonly ModalityClassService modalityClassService;
        private readonly ModalityPaymentTypeService modalityPaymentTypeService;
        private readonly RegistrationModalityClassService registrationModalityClassService;

        public StudentRegistrationService(StudentRegistrationRepository repository, UserLevelRepository userLevelRepository,
            UserStateRepository userStateRepository, UserProfileService userProfileService,
            UserService userService,
            ModalityClassService modalityClassService,
            ModalityPaymentTypeService modalityPaymentTypeService,
            RegistrationModalityClassService registrationModalityClassService) : this(repository)
        {
            this.Repository = repository;
            this.userLevelRepository = userLevelRepository;
            this.userStateRepository = userStateRepository;
            this.userProfileService = userProfileService;
            this.userService = userService;
            this.modalityClassService = modalityClassService;
            this.modalityPaymentTypeService = modalityPaymentTypeService;
            this.registrationModalityClassService = registrationModalityClassService;
        }

        public StudentRegistration GetStudentByUser(User user)
        {
            DbSet<StudentRegistration> queryable = this.Repository.GetDbSet();
            StudentRegistration student = queryable.FirstOrDefault(st => st.User.Id.Equals(user.Id));

            return student;
        }

        public async Task<bool> SaveStudentUser(StudentRegistration studentRegistration)
        {
            User user = studentRegistration.User;
            using (var context = this.Repository.GetDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    User savedUser = await this.userService.SaveAsync(user);

                    if (savedUser != null)
                    {
                        UserLevel userLevel = await this.userLevelRepository.GetById(2);
                        UserState userState = await this.userStateRepository.GetById(1);
                        UserProfile userProfile = this.userProfileService.GetUserProfileByUser(savedUser);
                        if (userProfile == null)
                        {
                            userProfile = new UserProfile();
                        }

                        userProfile.Login = savedUser.RegisterCode;
                        userProfile.Password = savedUser.Birthday.ToString();
                        userProfile.User = savedUser;
                        userProfile.UserLevel = userLevel;
                        userProfile.UserState = userState;

                        UserProfile savedUserProfile = await this.userProfileService.SaveUserProfileAsync(userProfile);
                        if (savedUserProfile == null)
                        {
                            dbContextTransaction.Rollback();
                            throw new Exception("Não foi possivel salvar o perfil de usuário.");
                        }

                        foreach (RegistrationModalityClass registrationModalityClass in studentRegistration.RegistrationModalityClasses)
                        {
                            registrationModalityClass.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
                            registrationModalityClass.StudentRegistration = studentRegistration;
                            registrationModalityClass.IsValid = false;
                            registrationModalityClass.ModalityClass = await this.modalityClassService.GetByIdAsync(registrationModalityClass.ModalityClassId);
                            registrationModalityClass.ModalityPaymentType = await this.modalityPaymentTypeService.GetByIdAsync(registrationModalityClass.ModalityPaymentTypeId);
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
                    }
                    else
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("Não foi possivel salvar o aluno.");
                    }

                    dbContextTransaction.Commit();
                    return true;
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

                    wasRemoved = await this.Repository.Remove(studentRegistration);

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
    }
}
