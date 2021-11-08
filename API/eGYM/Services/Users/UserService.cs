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
        private readonly CompanyUnitRepository companyUnitRepository;

        public CompanyUnitService CompanyUnitService { get; }

        public UserService(UserRepository repository, UserLevelRepository userLevelRepository,
            UserStateRepository userStateRepository, UserProfileService userProfileService, CompanyUnitRepository companyUnitRepository,
            ClaimResolver claimResolver
            ) : this(repository)
        {
            this.Repository = repository;
            this.userLevelRepository = userLevelRepository;
            this.userStateRepository = userStateRepository;
            this.userProfileService = userProfileService;
            this.companyUnitRepository = companyUnitRepository;
            this.claimResolver = claimResolver;
        }

        public async Task<User> ResolveUser()
        {
            IQueryable<User> queryable = this.Repository.GetQuery();
            long? userId = await this.claimResolver.ResolveUserIdAsync();

            User user = queryable.FirstOrDefault(u => u.Id == userId);

            return user;
        }

        public override async Task PreSavingRoutine(User entity)
        {
            entity.CompanyUnit = await this.companyUnitRepository.GetById((int)entity.CompanyUnitId);
        }
    }
}