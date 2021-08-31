using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using SalesManager.Model;
using Microsoft.EntityFrameworkCore;
using SalesManager.Models;

namespace SalesManager
{
    public class Startup
    {
        private const int V = 0;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Env = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<AppFeatures>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
                    ValidateAudience = false,
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
                options.AddPolicy("bStudioApps",
                    x => x.AllowAnyOrigin()
                    .WithHeaders("Content-Type", "Accept", "Origin", "Access-Control-Allow-Origin", "Authorization", "X-XSRF-TOKEN", "XSRF-TOKEN", "enctype", "info")
                    .DisallowCredentials()
                    .WithMethods("GET", "POST", "OPTIONS"));
            });
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
            //        SameSite = SameSiteMode.Strict,
            //    };
            //});
            services.AddSignalR(x => x.KeepAliveInterval = TimeSpan.FromSeconds(10));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
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
            //app.Use((context, next) =>
            //{
            //    string path = context.Request.Path.Value;
            //    if (context is null)
            //        antiforgery.SetCookieTokenAndHeader(context);
            //    var token = antiforgery.GetAndStoreTokens(context);
            //    context.Response.Cookies.Append("XSRF-TOKEN", token.RequestToken
            //        //new CookieOptions
            //        //{
            //        //    IsEssential = true,
            //        //    MaxAge = TimeSpan.FromDays(7),
            //        //    HttpOnly = false,
            //        //    SameSite = SameSiteMode.Strict,
            //        //}
            //        );
            //    return next();
            //});
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("bStuioApps");
            app.UseAuthentication();
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
