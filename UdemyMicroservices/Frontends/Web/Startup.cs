using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Models;
using Web.Services.Abstract;
using Web.Services.Concrete;

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
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            services.AddHttpClient<IIdentityService,IdentityService>();

            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    opts =>
                    {
                        opts.LoginPath = "/Auth/SignIn";
                        opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                        opts.SlidingExpiration = true;
                        opts.Cookie.Name = "webclientcookie";
                    });

            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}