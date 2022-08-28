using AlsoftShop.Factories;
using AlsoftShop.Factories.Interfaces;
using AlsoftShop.Repository;
using AlsoftShop.Repository.Interfaces;
using AlsoftShop.Services;
using AlsoftShop.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlsoftShop
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
            services.AddControllersWithViews();

            // signletons
            services.AddSingleton<IRepository, DatabaseRepository>();

            // scoped
            services.AddScoped<ISubtotalPriceService, SubtotalPriceService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<ITotalPriceService, TotalPriceService>();
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Shop}/{action=Index}/{id?}");
            });
        }
    }
}
