using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesManager.Helper
{
    public class AuthHelper
    {
        private IWebHostEnvironment Env { get; }
        private readonly IList<Claim> Claims;

        public AuthHelper(IList<Claim> claims, IWebHostEnvironment environment)
        {
            Env = environment;
            Claims = claims;

        }
        public string Key
        {
            get
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SxkeJZF776DgzfE!@"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: Env.IsProduction() ? "https://danitogames.com/" : "https://localhost:44326/",
                    audience: Env.IsProduction() ? "https://danitogames.com/" : "https://localhost:44326/",
                    claims: Claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return tokenString;
            }
        }
    }
}
