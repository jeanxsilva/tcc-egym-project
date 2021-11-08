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
    public class UserLevelRoleQuery
    {
        #region ListUserLevelRole()

        [UsePaging]
        [UseOffsetPaging(MaxPageSize = 1000, DefaultPageSize = 50, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        //[Authorize(Roles = new[] { "UserLevelRole.R" })]
        //[UseDbContext(typeof(EGymDbContext))]
        public virtual IQueryable<UserLevelRole> ListUserLevelRole(
            [Service] EGymDbContext dbContext)
        {
            return dbContext.Set<UserLevelRole>();
        }

        #endregion
    }
}
