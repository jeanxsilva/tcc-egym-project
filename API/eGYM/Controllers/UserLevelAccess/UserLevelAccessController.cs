using eGYM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace eGYM
{
    public partial class UserLevelAccessController
    {
        private readonly UserProfileService userProfileService;

        [ActivatorUtilitiesConstructor]
        public UserLevelAccessController(UserLevelAccessService service, UserProfileService userProfileService) : this(service)
        {
            this.userProfileService = userProfileService;
        }

        [HttpGet]
        [Authorize]
        [Route("GetLevelAccess")]
        public List<UserLevelAccess> GetLevelAccess()
        {
            var user = User.Identity;
            Claim claim = User.Claims.FirstOrDefault(c => c.Type.Equals("NameIdentifier"));
            //this.userProfileService.GetAuthenticatedUser();

            return this.Service.GetLevelAccess(1);
        }
    }
}