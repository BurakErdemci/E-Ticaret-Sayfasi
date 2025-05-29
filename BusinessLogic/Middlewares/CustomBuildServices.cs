using BusinessLogic.Services;
using Core.Abstract;
using Core.Abstract.IService;
using Core.Concretes.Entities;
using Data;
using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Middlewares
{
   public static class CustomBuildServices
    {
        public static IServiceCollection AddDataConnections(this IServiceCollection services, IConfiguration configuration ) 
        {
            services.AddDbContext<ShopContext>(options => options.UseSqlite
            (configuration.GetConnectionString("default")));
         
            services.AddIdentity<Member,MemberRoles>().AddEntityFrameworkStores<ShopContext>().AddDefaultTokenProviders();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

           
         

            
            return services;

        }
        public static IServiceCollection AddShowroomServices(this IServiceCollection services)
        {
            services.AddScoped<IShopServices, ShopService>();
            return services;
        }
        public static IServiceCollection AddPanelServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountServices, AccountServices>();

            services.AddScoped<ICategoryCrudServices, CategoryCrudServices>();
            services.AddScoped<IProductCrudServices, ProductCrudServices>();
            services.AddScoped<IBrandCrudServices, BrandCrudServices>();
           
            return services;
        }
        public static IServiceCollection AddCartAndOrderServices(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
