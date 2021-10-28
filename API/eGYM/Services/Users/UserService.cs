using eGYM.Core;
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
        private readonly ClaimResolver claimResolver;

        public UserService(UserRepository repository, UserLevelRepository userLevelRepository,
            UserStateRepository userStateRepository, UserProfileService userProfileService,
            ClaimResolver claimResolver
            ) : this(repository)
        {
            this.Repository = repository;
            this.userLevelRepository = userLevelRepository;
            this.userStateRepository = userStateRepository;
            this.userProfileService = userProfileService;
            this.claimResolver = claimResolver;
        }

        public async Task<User> ResolveUser()
        {
            IQueryable<User> queryable = this.Repository.GetQuery();
            long? userId = await this.claimResolver.ResolveUserIdAsync();

            User user = queryable.FirstOrDefault(u => u.Id == userId);

            return user;
        }
    }
}