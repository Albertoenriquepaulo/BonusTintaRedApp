using Blazored.Modal;
using BonusApp.Data;
using BonusApp.EmailSender;
using BonusApp.Services;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BonusApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllers();

            services.AddDbContext<CouponAppDbContext>(options =>
            {
                options.UseSqlite("Data Source = ClientCouponsDB.db");
            });

            services.AddLocalization();
            services.AddScoped<ClientServices>();
            services.AddScoped<CouponServices>();
            services.AddScoped<ClientCouponServices>();
            services.AddScoped<TransactionServices>();
            services.AddScoped<RefreshServices>();
            services.AddScoped<EmailHelper>();
            services.AddScoped<NotificationService>();

            services.AddBlazoredModal();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseHttpsRedirection();

            // Begin I18N configuration
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("es")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);
            // End I18N configuration

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            var opt = new BrowserWindowOptions
            {
                Icon = @"resources\bin\pdfs\templates\favicon.ico",
                AutoHideMenuBar = true,
                Title = "Tinta Red A Coruña - Coupon Control",
            };

            Task.Run(async () => await Electron.WindowManager.CreateWindowAsync(opt));
        }
    }
}