using Microsoft.IdentityModel.Tokens;
using eGYM.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eGYM.Services
{
    public static class ServiceToken
    {
        public static async Task<string> GenerateToken(UserProfile userProfile)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userProfile.User.Name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userProfile.Login));
            claims.Add(new Claim(ClaimTypes.PrimarySid, userProfile.User.Id.ToString()));

            CompanyUnit companyUnit = userProfile.User.CompanyUnit;
            if (companyUnit != null)
            {
                claims.Add(new Claim(ClaimTypes.Locality, userProfile.User.CompanyUnit.Id.ToString()));
            }

            foreach (UserLevelRole userLevelRole in userProfile.UserLevel.UserLevelRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userLevelRole.Role));
            }

            var handler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Settings.SecretByte),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }
    }
}
