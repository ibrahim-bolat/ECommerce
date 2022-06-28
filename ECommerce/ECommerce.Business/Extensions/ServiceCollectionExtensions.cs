using System.Reflection;
using ECommerce.Business.Validations.Identity;
using ECommerce.DataAccess.Concrete.EfCore.Contexts;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Helpers.MailHelper;
using ECommerce.Shared.Helpers.MailHelper;
using ECommerce.Shared.Service.Abtract;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Business.Extensions;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection LoadMyService(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        /*
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();
        */
        serviceCollection.AddDbContext<DataContext>(options =>
        {
            //dotnet ef migrations add InitialCreate -s ECommerce.MVC -p ECommerce.DataAccess
            //dotnet ef database update -s MyWebApp.MVC -p MyWebApp.DataAccess
            options.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        serviceCollection.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true; 
                options.User.AllowedUserNameCharacters =
                    "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
                options.SignIn.RequireConfirmedEmail = false; 
            }).AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        ;
        
        serviceCollection.ConfigureApplicationCookie(cookieOptions =>
        {
            cookieOptions.LoginPath = new PathString("/Admin/Account/Login");
            cookieOptions.LogoutPath = new PathString("/Admin/Account/Logout");
            cookieOptions.Cookie = new CookieBuilder
            {
                Name = "AspNetCoreIdentityCookie", 
                HttpOnly = false, 
                SameSite = SameSiteMode.Lax, 
                SecurePolicy = CookieSecurePolicy.Always 
            };
            cookieOptions.SlidingExpiration = true; 
            cookieOptions.ExpireTimeSpan =
                TimeSpan.FromDays(150);
            cookieOptions.AccessDeniedPath = new PathString("/ErrorPages/AccessDenied");
        });
        
        serviceCollection.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        }); 
        
        serviceCollection.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true; 
        });
        
        serviceCollection.AddFluentValidation(options =>
        {
           options.ImplicitlyValidateChildProperties = true;
           options.ImplicitlyValidateRootCollectionElements = true;
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        serviceCollection.Configure<MailSettings>(configuration.GetSection("MailSettings"));

        serviceCollection.AddTransient<IEmailService, EmailHelper>();
        
        return serviceCollection;
    }
}