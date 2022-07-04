using AutoMapper;
using FertilityPoint.BLL.Repositories.ApplicationUserModule;
using FertilityPoint.BLL.Repositories.AppointmentModule;
using FertilityPoint.BLL.Repositories.CountyModule;
using FertilityPoint.BLL.Repositories.MpesaStkModule;
using FertilityPoint.BLL.Repositories.PatientModule;
using FertilityPoint.BLL.Repositories.ServiceModule;
using FertilityPoint.BLL.Repositories.SpecialityModule;
using FertilityPoint.BLL.Repositories.SubCountyModule;
using FertilityPoint.BLL.Repositories.TimeSlotModule;
using FertilityPoint.DAL.MapperProfiles;
using FertilityPoint.DAL.Modules;
using FertilityPoint.Extensions;
using FertilityPoint.SeedAppUsers;
using FertilityPoint.Services.EmailModule;
using FertilityPoint.Services.MpesaC2BModule;
using FertilityPoint.Services.SMSModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace FertilityPoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(MapperProfile));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddScoped<IMailService, MailService>();

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            services.AddScoped<ICountyRepository, CountyRepository>();

            services.AddScoped<ISubCountyRepository, SubCountyRepository>();

            services.AddScoped<ISpecialityRepository, SpecialityRepository>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();

            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped<IMessagingService, MessagingService>();

            services.AddScoped<IServicesRepository, ServicesRepository>();

            services.AddScoped<IMpesaClient, MpesaClient>();

            services.AddMpesaService(Enums.Environment.Sandbox);

            //services.AddMpesaService(Enums.Environment.Live);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            SeedUsers.Seed(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllerRoute(
               name: "SuperAdmin",
               pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                name: "Admin",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }




    }
}
