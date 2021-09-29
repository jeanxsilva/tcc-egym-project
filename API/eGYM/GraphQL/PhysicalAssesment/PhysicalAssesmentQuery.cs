using eGYM.Models;
using eGYM.Services;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.GraphQL
{
    [ExtendObjectType(typeof(Query))]
    public class PhysicalAssesmentQuery
    {
        #region ListPhysicalAssesment()

        [UsePaging]
        [UseOffsetPaging(MaxPageSize = 1000, DefaultPageSize = 20, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        //[Authorize(Roles = new[] { "PhysicalAssesment.R" })]
        //[UseDbContext(typeof(EGymDbContext))]
        public virtual IQueryable<PhysicalAssesment> ListPhysicalAssesment(
            [Service] EGymDbContext dbContext)
        {
            return dbContext.Set<PhysicalAssesment>();
        }

        #endregion
    }
}
