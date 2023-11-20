using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(AppUser appUser , UserManager<AppUser> userManager)
        {
            // Custom Claims [ user-defined ]
            var customClaims = new List<Claim>()
            {
                new Claim (ClaimTypes.GivenName,appUser.DisplayName),
                new Claim(ClaimTypes.Email,appUser.Email)
            };

            var roles = await userManager.GetRolesAsync(appUser);

            foreach (var role in roles)
            {
                customClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(

                // Payload
                issuer: _configuration["JWT:Issure"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:Duration"])),
                audience: _configuration["JWT:Audience"],
                claims: customClaims,
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
