Set-Location C:\Users\jeanx\Documents\Git\TCC-Projeto\Documentação
$list = Get-Content ./Models-Name.txt

foreach($entityName in $list){
 $nameWithQuery = "$($entityName)Query";

 New-Item -Path C:\Users\jeanx\Documents\Git\TCC-Projeto\e-GYM\API\eGYM\GraphQL\ -Name $entityName -ItemType "directory"
 New-Item -Path C:\Users\jeanx\Documents\Git\TCC-Projeto\e-GYM\API\eGYM\GraphQL\$entityName -Name $nameWithQuery".cs" -ItemType "file"
 
Add-Content -Path C:\Users\jeanx\Documents\Git\TCC-Projeto\e-GYM\API\eGYM\GraphQL\$entityName\$nameWithQuery".cs" -Value @"
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
    public class $($entityName)Query
    {
        #region List$($entityName)()

        [UsePaging]
        [UseOffsetPaging(MaxPageSize = 1000, DefaultPageSize = 20, IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        //[Authorize(Roles = new[] { "$($entityName).R" })]
        //[UseDbContext(typeof(EGymDbContext))]
        public virtual IQueryable<$($entityName)> List$($entityName)(
            [Service] EGymDbContext dbContext)
        {
            return dbContext.Set<$($entityName)>();
        }

        #endregion
    }
}
"@
}