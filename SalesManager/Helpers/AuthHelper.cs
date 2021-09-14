using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SalesManager.Models;
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
        private readonly IAppFeatures App;

        public AuthHelper(IList<Claim> claims, IWebHostEnvironment environment, AppFeatures app)
        {
            Env = environment;
            Claims = claims;
            App = app;
        }
        public string Key
        {
            get
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(App.Key));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: App.Issuer,
                    audience: App.Audience,
                    claims: Claims,
                    expires: App.Expiry,
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return tokenString;
            }
        }
    }
}
