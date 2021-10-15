using eGYM.Database.Repositories;
using eGYM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class UserService
    {
        private readonly UserLevelRepository userLevelRepository;
        private readonly UserStateRepository userStateRepository;
        private readonly UserProfileService userProfileService;
        //private readonly StudentRegistrationService studentRegistrationService;

        public UserService(UserRepository repository, UserLevelRepository userLevelRepository,
            UserStateRepository userStateRepository, UserProfileService userProfileService 
            ) : this(repository)
        {
            this.Repository = repository;
            this.userLevelRepository = userLevelRepository;
            this.userStateRepository = userStateRepository;
            this.userProfileService = userProfileService;
            //this.studentRegistrationService = studentRegistrationService;
        }

        //public async Task<bool> SaveStudent(User user)
        //{
        //    using (var context = this.Repository.GetDbContext())
        //    {
        //        using (var dbContextTransaction = context.Database.BeginTransaction())
        //        {
        //            User savedUser = await this.Repository.InsertOrUpdate(user);

        //            if (savedUser != null)
        //            {
        //                UserLevel userLevel = await this.userLevelRepository.GetById(2);
        //                UserState userState = await this.userStateRepository.GetById(1);

        //                UserProfile userProfile = this.userProfileService.GetUserProfileByUser(savedUser);
        //                if (userProfile == null)
        //                {
        //                    userProfile = new UserProfile();
        //                }

        //                userProfile.Login = user.RegisterCode;
        //                userProfile.Password = user.Birthday.ToString();
        //                userProfile.User = savedUser;
        //                userProfile.UserLevel = userLevel;
        //                userProfile.UserState = userState;

        //                UserProfile savedUserProfile = await this.userProfileService.SaveUserProfileAsync(userProfile);
        //                if (savedUserProfile == null)
        //                {
        //                    dbContextTransaction.Rollback();
        //                    throw new Exception("Não foi possivel salvar o perfil de usuário.");
        //                }

        //                StudentRegistration studentRegistration = this.studentRegistrationService.GetStudentByUser(user);
        //                if (studentRegistration == null)
        //                {
        //                    studentRegistration = new StudentRegistration();
        //                }

        //                studentRegistration.User = savedUser;
        //                studentRegistration.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
        //                studentRegistration.Code = DateTime.UtcNow.Year.ToString() + DateTime.UtcNow.Month.ToString() + user.RegisterCode;

        //                StudentRegistration savedStudentRegistration = await this.studentRegistrationService.SaveAsync(studentRegistration);
        //                if (savedStudentRegistration == null)
        //                {
        //                    dbContextTransaction.Rollback();
        //                    throw new Exception("Não foi possivel salvar a matricula do aluno.");
        //                }
        //            }
        //            else
        //            {
        //                dbContextTransaction.Rollback();
        //                throw new Exception("Não foi possivel salvar o aluno.");
        //            }

        //            dbContextTransaction.Commit();
        //            return true;
        //        }
        //    }
        //}
    }
}