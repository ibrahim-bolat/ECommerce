using System.Reflection;
using System.Text.Json.Serialization;
using ECommerce.Business.Abstract;
using ECommerce.Business.Concrete;
using ECommerce.Business.Validations.Identity;
using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete;
using ECommerce.DataAccess.Concrete.EfCore.Contexts;
using ECommerce.DataAccess.Concrete.EfCore.Repository;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Helpers.MailHelper;
using ECommerce.Shared.Models;
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
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.AddControllersWithViews().AddRazorRuntimeCompilation();
        serviceCollection.AddControllersWithViews().AddFluentValidation(options =>
        {
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        serviceCollection.AddControllers().AddJsonOptions(options => 
        { 
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });
        
        serviceCollection.AddDbContext<DataContext>(options =>
        {
            //dotnet ef migrations add InitialCreate -s ECommerce.MVC -p ECommerce.DataAccess
            //dotnet ef database update -s MyWebApp.MVC -p MyWebApp.DataAccess
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        
        serviceCollection.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                    "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
                options.SignIn.RequireConfirmedEmail = false; 
                options.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

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

        serviceCollection.Configure<MailSettings>(configuration.GetSection("MailSettings"));

        serviceCollection.AddTransient<IEmailService, EmailHelper>();
        
        
        //serviceCollection.AddScoped<DbContext, DataContext>(); 
        //serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));

        //repositories
        serviceCollection.AddScoped<IAddressRepository,EfAddressRepository>();
        serviceCollection.AddScoped<IUserImageRepository,EfUserImageRepository>();
        serviceCollection.AddScoped<IUserRepository,EfUserRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        
        //services
        serviceCollection.AddScoped<IAddressService, AddressManager>();
        serviceCollection.AddScoped<IUserImageService, UserImageManager>();
        serviceCollection.AddScoped<IUserService, UserManager>();

        return serviceCollection;
    }
}