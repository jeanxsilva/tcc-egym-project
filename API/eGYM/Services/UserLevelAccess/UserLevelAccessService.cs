using eGYM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM
{
    public partial class UserLevelAccessService
    {
        public List<UserLevelAccess> GetLevelAccess(long userLevelId)
        {
            IQueryable<UserLevelAccess> queryable = this.Repository.GetQuery();
            List<UserLevelAccess> userLevelAcesses = queryable.Where(ula => ula.UserLevel.Id == userLevelId).ToList();

            return userLevelAcesses;
        }
    }
}
