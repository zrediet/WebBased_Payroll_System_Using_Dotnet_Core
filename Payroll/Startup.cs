using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using Microsoft.Extensions.DependencyInjection.Extensions;
//using Telerik.Reporting.Cache.File;
//using Telerik.Reporting.Services;

namespace Payroll
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDevExpressControls();
            services.AddMvc();

            services.ConfigureReportingServices(configurator => {
                configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
                    viewerConfigurator.UseCachedReportSourceBuilder();
                });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages();

            //services.TryAddSingleton<IReportServiceConfiguration>(sp => 
            //    new ReportServiceConfiguration
            //{
            //    //Storage = new FileStorage(),
            //    //ReportSourceResolver = new UriReportSourceResolver(
            //    //    System.IO.Path.Combine(
            //    //        sp.GetService<IWebHostEnvironment>().ContentRootPath,
            //    //        "Reports"))

            //    //ReportingEngineConfiguration = ConfigurationHelper.ResolveConfiguration(sp.GetService<IWebHostEnvironment>()),
            //   // ReportingEngineConfiguration = sp.GetService<IConfiguration>(),

            //   Storage = new FileStorage(),
            //   ReportSourceResolver = new UriReportSourceResolver(
            //       System.IO.Path.Combine(
            //           sp.GetService<IWebHostEnvironment>().ContentRootPath,
            //           "Reports"))


            //    //HostAppId = "ReportingCore5App",
            //    //Storage = new FileStorage(),
            //    //ReportSourceResolver = new TypeReportSourceResolver()
            //    //    .AddFallbackResolver(new UriReportSourceResolver())
            //});


            // Configure dependencies for ReportsController.
            //services.TryAddSingleton<IReportServiceConfiguration>(sp =>
            //    new ReportServiceConfiguration
            //    {
            //        //ReportingEngineConfiguration = ConfigurationHelper.ResolveConfiguration(sp.GetService<IWebHostEnvironment>()),
            //        HostAppId = "Net5RestServiceWithCors",
            //        Storage = new FileStorage(),
            //        ReportSourceResolver = new UriReportSourceResolver(
            //            System.IO.Path.Combine(sp.GetService<IWebHostEnvironment>().ContentRootPath, "Reports"))
            //    });



            services.AddControllers();

            services.AddCors(corsOption => corsOption.AddPolicy(
                "ReportingRestPolicy",
                corsBuilder =>
                {
                    corsBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDevExpressControls();
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            

            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapRazorPages();

                endpoints.MapControllers();

            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });


        }

        
    }
}
