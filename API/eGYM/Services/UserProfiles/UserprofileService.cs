using eGYM.Controllers;
using eGYM.Core;
using eGYM.Database.Repositories;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class UserProfileService
    {
        private readonly UserRepository userRepository;
        private readonly ClaimResolver claimResolver;
        private readonly SecurityHash securityHash;

        public UserProfileService(UserProfileRepository repository, UserRepository userRepository, ClaimResolver claimResolver) : this(repository)
        {
            this.Repository = repository;
            this.userRepository = userRepository;
            this.claimResolver = claimResolver;
            this.securityHash = new SecurityHash(SHA256.Create());
        }

        public async Task<UserProfile> ResolveUserProfile()
        {
            IQueryable<UserProfile> queryable = this.Repository.GetQuery();
            string userLogin = await this.claimResolver.ResolveUserLoginAsync();

            UserProfile userProfile = queryable.FirstOrDefault(up => up.Login.Equals(userLogin));

            return userProfile;
        }

        public async Task<UserProfile> AuthenticateAsync(UserLogin userLogin)
        {
            IQueryable<UserProfile> queryable = this.Repository.GetQuery();

            UserProfile userProfile = queryable.FirstOrDefault(up => up.Login.Equals(userLogin.Username));

            if (userProfile != null)
            {
                if (this.securityHash.CompairPassword(userProfile.PasswordEncrypted, userLogin.Password)) //encripta e compara
                {
                    return await this.Repository.GetById(userProfile.Id);
                }
            }

            return null;
        }

        public UserProfile GetUserProfileByUser(User user)
        {
            IQueryable<UserProfile> queryable = this.Repository.GetQuery();
            UserProfile userProfile = queryable.FirstOrDefault(up => up.User.Id.Equals(user.Id));

            if (userProfile != null)
            {
                return userProfile;
            }

            return null;
        }

        public async Task<UserProfile> SaveUserProfileAsync(UserProfile userProfile)
        {
            userProfile.PasswordEncrypted = this.securityHash.CryptoPassword(userProfile.Password);
            UserProfile savedUserProfile = await this.SaveAsync(userProfile);

            return savedUserProfile;
        }
    }
}