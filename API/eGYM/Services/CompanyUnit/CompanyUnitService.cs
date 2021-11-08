using eGYM.Core;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class CompanyUnitService
    {
        private readonly ClaimResolver claimResolver;
        private readonly UserService userService;

        public CompanyUnitService(CompanyUnitRepository repository, ClaimResolver claimResolver, UserService userService) : this(repository)
        {
            this.claimResolver = claimResolver;
            this.userService = userService;
        }

        public async Task<CompanyUnit> ResolveCompanyUnit()
        {
            IQueryable<CompanyUnit> queryable = this.Repository.GetQuery();
            long? companyUnitId = await this.claimResolver.ResolveCompanyUnitIdAsync();

            if (companyUnitId != null)
            {
                CompanyUnit companyUnit = queryable.FirstOrDefault(unit => unit.Id == companyUnitId);

                return companyUnit;
            }
            else
            {
                User currentUser = await this.userService.ResolveUser();
                CompanyUnit companyUnit = currentUser.CompanyUnit;

                return companyUnit;
            }

            return null;
        }
    }
}
