using Microsoft.EntityFrameworkCore;
using Saaya.Web.Db;
using Saaya.Web.Services;
using Westwind.AspNetCore.Markdown;

namespace Saaya.Web
{
    public class Saaya
    {
        public IConfiguration Configuration { get; }

        public Saaya(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMarkdown();
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddSession();

            // To Do: Add Db + GitHub OAuth login
            services.AddDbContext<SaayaWebContext>(options => options.UseSqlite("Data Source=saaya.db"));
            services.AddScoped<SaayaWebContext>();
            services.AddScoped<DiscordLogin>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseMarkdown();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(x =>
            {
                x.MapControllerRoute(
                    name: "blog",
                    pattern: "blog/{id}",
                    defaults: new { controller = "blog", action = "post" });

                x.MapControllerRoute(
                    name: "blog",
                    pattern: "blog",
                    defaults: new { controller = "blog", action = "index" });

                x.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}");
            });
            
            // Jank
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<SaayaWebContext>();
                db.Database.EnsureCreated();
            }
        }
    }
}