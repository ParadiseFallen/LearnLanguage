using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Data;
using Web.Models;
using Web.Services;
using System.Text.Json;
using Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Web
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
            //add services
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(true);
            });

            services.AddAuthorizationCore(options => 
            {
                //options.AddPolicy()
                options.AddPolicy("Admin",policy=>policy.RequireRole("Admin"));
                options.AddPolicy("Moderator", policy => policy.RequireAssertion(context=>context.User.IsInRole("Moderator")));

            });
            services.AddAuthorizationCore();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

            })
                .AddEntityFrameworkStores<DatabaseContext>();

            services.ConfigureApplicationCookie(config =>
            {
                config.Events.DisableRedirectForPath(e => e.OnRedirectToLogin,
                "/api", StatusCodes.Status401Unauthorized);
                config.Events.DisableRedirectForPath(e =>
                e.OnRedirectToAccessDenied,
                "/api", StatusCodes.Status403Forbidden);
            });

            services.AddServerSideBlazor();

            services.AddRazorPages()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.AddAllConverters());

            //services.AddControllers().ConfigureApiBehaviorOptions(x=>x.);

            #region register services
            services.AddSingleton<WeatherForecastService>(); //REMOVE

            services.AddScoped<TTSService>();
            services.AddScoped<TranslationService>();
            services.AddScoped<AccountService>();
            services.AddScoped<PhraseService>();
            services.AddScoped<LanguageService>();

            #endregion
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToPage("/_Host");
            });

        }
    }
}
