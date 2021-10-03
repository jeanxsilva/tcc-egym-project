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
        private readonly SecurityHash securityHash;

        public UserProfileService(UserProfileRepository repository, UserRepository userRepository): this(repository)
        {
            this.Repository = repository;
            this.userRepository = userRepository;
            this.securityHash = new SecurityHash(SHA512.Create());
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
    }
}
