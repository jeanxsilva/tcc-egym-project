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

            if (userProfile != null)
            {
                string token = await ServiceToken.GenerateToken(userProfile);

                returnBag.Token = token;
                returnBag.ExpiresIn = 2;

                userProfile.PasswordEncrypted = null;
                userProfile.User.CompanyUnit = new CompanyUnit();
                userProfile.User.ModalityClasses.Clear();
                userProfile.User.PaymentPaidByUsers.Clear();
                userProfile.User.PaymentReceivedByUsers.Clear();
                userProfile.User.PaymentReversalCreatedByUsers.Clear();
                userProfile.User.StudentRequests.Clear();
                userProfile.User.CompanyUnits.Clear();
                userProfile.User.LastNews.Clear();
                userProfile.User.StudentRegistration = null;
                userProfile.UserLevel.UserLevelRoles.Clear();
                userProfile.UserLevel.UserLevelAccesses.Clear();
                userProfile.UserState = new UserState();

                returnBag.UserProfile = userProfile;
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
