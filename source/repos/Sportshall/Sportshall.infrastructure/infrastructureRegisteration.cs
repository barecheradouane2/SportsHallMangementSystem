using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.Core.Services;
using Sportshall.infrastructure.Data;
using Sportshall.infrastructure.Repositries;
using Sportshall.infrastructure.Repositries.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure
{
    public static class infrastructureRegisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            //  services.AddTransient  fi7al makanch save like email 
            //services.addscoped fi7al  http db context 
            //services.addsingleton fi7al  dima yamchi vedio 10

            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            //apply unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IGenerateToken, GenerateToken>();

            //services.AddScoped<IAuth, AuthRepositry>();


            services.AddSingleton<IImageMangementService, ImageMangementService>();

            services.AddScoped <ISubscriptionService, SubscriptionService>();

            services.AddScoped<IProductSalesService, ProductSalesService>();    

            services.AddSingleton<IFileProvider> (new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SportshallDataBase"));
            });

           


            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(op =>
            {

                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                op.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;


            }).AddCookie( o =>
            {
                o.Cookie.Name = "token";

                o.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Unauthorized
                    return Task.CompletedTask;
                };


            }
                
         ).AddJwtBearer(

                op =>
                {
                    op.RequireHttpsMetadata = false;

                    op.SaveToken = true;

                    op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["Token:Issuer"],
                        //ValidAudience = configuration["JWT:ValidAudience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"])),

                        ClockSkew = TimeSpan.Zero // Disable the default clock skew of 5 minutes
                    };

                    op.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                         {
                             
                                 context.Token = context.Request.Query["token"];
                             
                             return Task.CompletedTask;
                         }
                    };
                }
                
                
                
                
                );





            return services;
            
        }
    }
}
