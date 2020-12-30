using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AboutVintage.Data;
using Blazored.Modal;
using Blazored.Toast;
using DataAccessLibrary;
using EfDataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AboutVintage
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

            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<IStoreData, StoreData>();
            services.AddTransient<IComplaintData, ComplaintData>();

            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

            services.AddDbContext<OrderComplaintContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScoped<OrderComplaintContextServices>();

            services.AddBlazoredModal();
            services.AddBlazoredToast();

            services.AddAuthentication("Cookies")
                .AddCookie(opt =>
                {
                    opt.Cookie.Name = "GoogleOAuth";
                    opt.LoginPath = "/auth/google-login";
                    //opt.ExpireTimeSpan= TimeSpan.FromSeconds(10);
                    opt.LogoutPath = "/auth/google-signout";
                })
                .AddGoogle(opt =>
                {
                    opt.ClientId = Configuration["Google:Id"];
                    opt.ClientSecret = Configuration["Google:Secret"];
                    opt.Scope.Add("profile");
                    opt.Events.OnCreatingTicket = (context =>
                    {
                        string picuri = context.User.GetProperty("picture").GetString();

                        context.Identity.AddClaim(new Claim("picture", picuri));

                        return Task.CompletedTask;
                    });

                });
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
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
