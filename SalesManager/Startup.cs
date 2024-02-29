using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using SalesManager.Model;
using Microsoft.EntityFrameworkCore;
using SalesManager.Models;
using Microsoft.Extensions.Logging;

namespace SalesManager
{
    public class Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        public IConfiguration Configuration { get; } = configuration;
        public IWebHostEnvironment Env { get; set; } = environment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AppFeatures>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //options.EnableSensitiveDataLogging();
                //options.LogTo(x => Console.WriteLine(x));
                //options.log
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(x =>
            {
                x.SignIn.RequireConfirmedAccount = false;
                x.Password.RequiredLength = 8;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireDigit = true;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = true;
                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            //var audience = Configuration.GetSection("AppFeatures").GetSection("Audience").Value;
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppFeatures").GetSection("Key").Value)),
                    ValidateIssuer = true,
                    RequireExpirationTime = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration.GetSection("AppFeatures").GetSection("Issuer").Value,
                    ValidAudience = Configuration.GetSection("AppFeatures").GetSection("Audience").Value
                };
            });
            services.AddDataProtection();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
            });
            services.AddCors(options =>
            {
                options.AddPolicy("bStudioApps", x =>
                x.WithOrigins("http://localhost:4200", "http://localhost:807")
                .WithHeaders("Content-Type", "Accept", "Origin", "Authorization", "X-XSRF-TOKEN", "enctype", "Access-Control-Allow-Origin", "Access-Control-Allow-Credentials")
                .WithMethods("GET", "POST", "PUT", "OPTIONS", "DELETE")
                .AllowCredentials());
            });
            services.AddLogging(x => x.AddConsole());
            services.AddSignalR(x => x.KeepAliveInterval = TimeSpan.FromSeconds(10));
            //services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            //services.AddAntiforgery(o =>
            //{
            //    o.HeaderName = "X-XSRF-TOKEN";
            //    o.Cookie = new CookieBuilder
            //    {
            //        Expiration = TimeSpan.FromDays(7),
            //        IsEssential = true,
            //        MaxAge = TimeSpan.FromDays(7),
            //        HttpOnly = false,
            //        Name = "XSRF-TOKEN",
            //        SameSite = SameSiteMode.None,
            //    };
            //});
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("ValidRequestMW");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.Use((context, next) =>
            {
                string path = context.Request.Path.Value;
                if (
                    string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase))
                {
                    //antiforgery.SetCookieTokenAndHeader(context);
                    //var token = antiforgery.GetAndStoreTokens(context);
                    //var res = antiforgery.ValidateRequestAsync(context);
                    //if (res.IsFaulted)
                    //{
                    //    Console.WriteLine($"Exception message is {res.Exception.Message}");
                    //}
                    //context.Response.Cookies.Append("XSRF-TOKEN", token.RequestToken);
                }
                return next(context);

            });
            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("bStuioApps");
            //app.UseAntiforgery();
            //app.EnsureAntiforgeryTokenPresentOnPosts();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
