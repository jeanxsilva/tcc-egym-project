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
        private readonly StudentRegistrationRepository studentRegistrationRepository;

        public UserService(UserRepository repository, UserLevelRepository userLevelRepository,
            UserStateRepository userStateRepository, UserProfileService userProfileService,
            StudentRegistrationRepository studentRegistrationRepository) : this(repository)
        {
            this.Repository = repository;
            this.userLevelRepository = userLevelRepository;
            this.userStateRepository = userStateRepository;
            this.userProfileService = userProfileService;
            this.studentRegistrationRepository = studentRegistrationRepository;
        }

        public async Task<bool> InsertNewStudentAsync(User user)
        {
            DbSet<User> dbSet = this.Repository.GetDbSet();
            User userExistent = dbSet.FirstOrDefault(u => u.RegisterCode == user.RegisterCode);

            if (userExistent == null)
            {
                User createdUser = await this.Repository.Create(user);

                if (createdUser != null)
                {
                    UserProfile userProfile = new UserProfile();
                    StudentRegistration studentRegistration = new StudentRegistration();

                    UserLevel userLevel = await this.userLevelRepository.GetById(2);
                    UserState userState = await this.userStateRepository.GetById(1);
                    userProfile.Login = user.RegisterCode;
                    userProfile.Password = user.Birthday.ToString();
                    userProfile.User = createdUser;
                    userProfile.UserLevel = userLevel;
                    userProfile.UserState = userState;

                    UserProfile createdUserProfile = await this.userProfileService.CreateUserProfileAsync(userProfile);

                    studentRegistration.User = createdUser;
                    studentRegistration.RegisterDateTime = DateTime.UtcNow.ToLocalTime();
                    studentRegistration.Code = DateTime.UtcNow.Year.ToString() + DateTime.UtcNow.Month.ToString() + user.RegisterCode;

                    StudentRegistration createdStudentRegistration = await this.studentRegistrationRepository.Create(studentRegistration);
                    if (createdStudentRegistration != null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                throw new Exception("O CPF informado já esta registrado no sistema");
            }

            return false;
        }
    }
}
