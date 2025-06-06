using Business.IService;
using Business.Service;
using Data.IRepository;
using Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using Model.PayOS;
using System.Text.Json;

namespace VieGo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ViegoDb1Context>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DBContext")));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            // Register repositories and services
            builder.Services.AddScoped<IDiscountCodeRepository, DiscountCodeRepository>();
            builder.Services.AddScoped<IDiscountCodeService, DiscountCodeService>();
            builder.Services.AddScoped<ITourRepository, TourRepository>();

            builder.Services.AddScoped<ITourService, TourService>();

            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBookingService, BookingService>();

            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();

            builder.Services.AddScoped<IAdminDashboardRepository, AdminDashboardRepository>();
            builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IProviderServiceBookingRepository, ServiceProviderBookingRepository>();
            builder.Services.AddScoped<IServiceProviderBookingService, ServiceProviderBookingService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddScoped<IRegisterService, RegisterService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.Configure<PayOSConfig>(
                 builder.Configuration.GetSection("PayOS")
             );

            builder.Services.AddScoped<PayOSService>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });


            builder.Services.AddControllersWithViews()
                   .AddJsonOptions(options =>
                   {
                       options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                       options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                       options.JsonSerializerOptions.MaxDepth = 64;
                   });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
