using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eGYM.Core
{
    public class ClaimResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClaimResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Claim>> GetClaimsAsync()
        {
            try
            {
                string token = await this.httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                
                if (string.IsNullOrEmpty(token))
                {
                    token = await this.httpContextAccessor.HttpContext.GetTokenAsync("id_token");
                }

                if (!string.IsNullOrEmpty(token) && token != "undefined")
                {
                    try
                    {
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        JwtSecurityToken securityToken = handler.ReadToken(token) as JwtSecurityToken;

                        if (securityToken != null && securityToken.Claims != null)
                        {
                            return securityToken.Claims.ToList();
                        }
                    }
                    catch (Exception exception)
                    {
                        throw;
                    }
                }

                return null;
            }
            catch (Exception exception)
            {
                throw new Exception("Fail in GetClaims()", exception);
            }
        }

        public async Task<string> ResolveUserLoginAsync()
        {
            List<Claim> claims = await this.GetClaimsAsync();
            Claim userLogin = claims.FirstOrDefault(c => c.Type == "nameid");

            if (userLogin != null)
            {
                return userLogin.Value;
            }

            return null;
        }

        public async Task<long?> ResolveCompanyUnitIdAsync()
        {
            List<Claim> claims = await this.GetClaimsAsync();
            Claim companyUnitId = claims.FirstOrDefault(c => c.Type == "locality" || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality");

            if (companyUnitId != null)
            {
                return long.Parse(companyUnitId.Value);
            }

            return null;
        }

        public async Task<long?> ResolveUserIdAsync()
        {
            List<Claim> claims = await this.GetClaimsAsync();
            Claim userId = claims.FirstOrDefault(c => c.Type == "primarysid");

            if (userId != null)
            {
                return long.Parse(userId.Value);
            }

            return null;
        }
    }
}
