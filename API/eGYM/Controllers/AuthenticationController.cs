﻿using eGYM.Models;
using eGYM.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace eGYM.Controllers
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

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
                userProfile.PasswordEncrypted = null;

                string token = ServiceToken.GenerateToken(userProfile);

                returnBag.Token = token;
                returnBag.ExpiresIn = 2;
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
