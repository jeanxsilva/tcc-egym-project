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
        public static string GenerateToken(UserProfile userProfile)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userProfile.User.Name));

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
