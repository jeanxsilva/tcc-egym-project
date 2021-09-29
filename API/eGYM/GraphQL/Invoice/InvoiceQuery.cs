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
    public class InvoiceQuery
    {
        #region ListInvoice()

        [UsePaging]
        [UseOffsetPaging(MaxPageSize = 1000, DefaultPageSize = 20, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        //[Authorize(Roles = new[] { "Invoice.R" })]
        //[UseDbContext(typeof(EGymDbContext))]
        public virtual IQueryable<Invoice> ListInvoice(
            [Service] EGymDbContext dbContext)
        {
            return dbContext.Set<Invoice>();
        }

        #endregion
    }
}
