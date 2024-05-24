using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjectionWeb
    {
        public static IServiceCollection AddInfrastructureWeb(this IServiceCollection services, IConfiguration configuration)
        {
            IncludeDataContext(services, configuration);
            
            ConfigureCookie(services);

            IncludeMappings(services);

            IncludeAutoMapper(services);

            IncludeMediatR(services);

            IncludeIdentity(services);

            return services;
        }

        private static void ConfigureCookie(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Account/Login");
        }

        private static void IncludeIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        }

        private static void IncludeMediatR(IServiceCollection services)
        {
            var myHandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myHandlers));
        }

        private static void IncludeAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
        }

        private static void IncludeMappings(IServiceCollection services)
        {
            IncludeRepositoriesMappings(services);

            IncludeServicesMappings(services);

            IncludeIdentityMappings(services);
        }

        private static void IncludeIdentityMappings(IServiceCollection services)
        {
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<IISeedUserRoleInitial, SeedUserRoleInitial>();
        }

        private static void IncludeRepositoriesMappings(IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        private static void IncludeServicesMappings(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }

        private static void IncludeDataContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                            (i => i.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                                                )
                            );
        }

    }
}
