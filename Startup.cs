using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlowOutRentalsPrep.Data;
using BlowOutRentalsPrep.Models;
using BlowOutRentalsPrep.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlowOutRentalsPrep
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            // var builder = new ConfigurationBuilder()
            //     .SetBasePath(env.ContentRootPath)
            //     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //     .AddEnvironmentVariables();
            // Configuration = builder.Build();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            var connection = "Data Source=sqlsv-instance1.ce61i890rwzw.us-west-2.rds.amazonaws.com,1433; Initial Catalog=BlowOutRentals; Persist Security Info=True; User ID=sqlsv_i1_admin; Password=goKCaG86rsKVhtET3;";
        	services.AddDbContext<BlowOutRentalsContext>(options => options.UseSqlServer(connection));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // app.Use(async (context, next) =>
            // {
            //     if (context.Request.IsHttps)
            //     {
            //         await next();
            //     }
            //     else
            //     {
            //         var httpsUrl = "https://" + context.Request.Host + context.Request.Path;
            //         context.Response.Redirect(httpsUrl);
            //     }
            // });


            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseGoogleAuthentication(new GoogleOptions
            {
                ClientId = "843371679847-h6a6s7841v65cll7v5dv96hj3e5cuajt.apps.googleusercontent.com",
                ClientSecret = "9QG6g6jAW9MIU0KoPK2WhNmG",
                Scope = { "email", "openid" }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "updatedata",
                    template: "{controller=Home}/{action=UpdateData}/{id?}");

                routes.MapRoute(
                    name: "rentals",
                    template: "{controller=Rentals}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "requestinfo",
                    template: "{controller=Rentals}/{action=RequestInformation}/{id?}");

                routes.MapRoute(
                    name: "about",
                    template: "{controller=About}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "contact",
                    template: "{controller=Contact}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "email",
                    template: "{controller=Contact}/{action=email}/{id?}");

                routes.MapRoute(
                    name: "admin",
                    template: "{controller=admin}/{action=Index}/{id?}");
            });
        }
    }
}
