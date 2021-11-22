using eGYM.Models;
using eGYM.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserProfileService userProfileService;

        public AuthenticationController(UserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<dynamic> AuthenticateAsync([FromBody] UserLogin userLogin)
        {
            dynamic returnBag = new ExpandoObject();
            returnBag.HasError = false;
            UserProfile userProfile = await this.userProfileService.AuthenticateAsync(userLogin);

            if (userProfile != null && userProfile.UserLevel.Id != (int)UserLevelEnum.Student)
            {
                string token = await ServiceToken.GenerateToken(userProfile);

                returnBag.Token = token;
                returnBag.ExpiresIn = 6;

                UserProfile returnedUserProfile = new UserProfile();
                returnedUserProfile.Id = userProfile.Id;
                returnedUserProfile.Login = userProfile.Login;

                UserLevel userLevel = new UserLevel();
                userLevel.Id = userProfile.UserLevel.Id;
                userLevel.RoleCode = userProfile.UserLevel.RoleCode;

                returnedUserProfile.UserLevel = userLevel;

                User returnedUser = new User();
                returnedUser.Id = userProfile.User.Id;
                returnedUser.Name = userProfile.User.Name;
                returnedUser.LastName = userProfile.User.LastName;
                returnedUser.CompanyUnit = new CompanyUnit();

                if (userProfile.User.CompanyUnit != null)
                {
                    returnedUser.CompanyUnit.Id = userProfile.User.CompanyUnit.Id;
                }

                returnedUserProfile.User = returnedUser;

                returnBag.UserProfile = returnedUserProfile;
            }
            else
            {
                returnBag.HasError = true;
                returnBag.Message = "As credenciais fornecidas estão incorretas.";
            }

            return returnBag;
        }


        [HttpPost]
        [Route("AuthenticateStudent")]
        public async Task<dynamic> AuthenticateStudent([FromBody] UserLogin userLogin)
        {
            dynamic returnBag = new ExpandoObject();
            returnBag.HasError = false;
            UserProfile userProfile = await this.userProfileService.AuthenticateAsync(userLogin);

            if (userProfile != null && userProfile.UserLevel.Id == (int)UserLevelEnum.Student)
            {
                string token = await ServiceToken.GenerateToken(userProfile);

                returnBag.Token = token;
                returnBag.ExpiresIn = 6;

                UserProfile returnedUserProfile = new UserProfile();
                returnedUserProfile.Id = userProfile.Id;
                returnedUserProfile.Login = userProfile.Login;

                UserLevel userLevel = new UserLevel();
                userLevel.Id = userProfile.UserLevel.Id;
                userLevel.RoleCode = userProfile.UserLevel.RoleCode;

                returnedUserProfile.UserLevel = userLevel;

                User returnedUser = new User();
                returnedUser.Id = userProfile.User.Id;
                returnedUser.Name = userProfile.User.Name;
                returnedUser.CompanyUnit = new CompanyUnit();

                if (userProfile.User.CompanyUnit != null)
                {
                    returnedUser.CompanyUnit.Id = userProfile.User.CompanyUnit.Id;
                }

                returnedUserProfile.User = returnedUser;

                returnBag.UserProfile = returnedUserProfile;
            }
            else
            {
                returnBag.HasError = true;
                returnBag.Message = "As credenciais fornecidas estão incorretas.";
            }

            return returnBag;
        }
    }
}
