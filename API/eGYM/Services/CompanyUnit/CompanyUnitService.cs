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

        public CompanyUnitService(CompanyUnitRepository repository, ClaimResolver claimResolver) : this(repository)
        {
            this.claimResolver = claimResolver;
        }

        public async Task<CompanyUnit> ResolveCompanyUnit()
        {
            IQueryable<CompanyUnit> queryable = this.Repository.GetQuery();
            long? companyUnitId = await this.claimResolver.ResolveCompanyUnitIdAsync();

            if (companyUnitId != null)
            {
                CompanyUnit companyUnit = queryable.FirstOrDefault(unit => unit.Id.Equals(companyUnitId));

                return companyUnit;
            }

            return null;
        }
    }
}
