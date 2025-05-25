using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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

            services.AddSingleton<IImageMangementService, ImageMangementService>();

            services.AddScoped <ISubscriptionService, SubscriptionService>();

            services.AddScoped<IProductSalesService, ProductSalesService>();    

            services.AddSingleton<IFileProvider> (new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SportshallDataBase"));
            });




            return services;
            
        }
    }
}
