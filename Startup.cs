using Advantage.Api;
using Advantage.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Advantage.API
{
    public class Startup
    {
        // private string _connectionString = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
                 {
                     opt.AddPolicy("CorsPolicy",
                         b => b.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials());
                 }
             );

            services.AddMvc();

            // services.AddEntityFrameworkNpgsql().AddDbContext<ApiContext>(opt => opt.UseNpgsql(_connectionString));

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApiContext>(
                    opt => opt.UseNpgsql(Configuration.GetConnectionString("AdvantageDemo")));

            services.AddTransient<DataSeed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataSeed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");


            var nCustomers = 20;
            var nOrders = 1000;
            seed.SeedData(nCustomers, nOrders);


            app.UseMvc(routes =>
               routes.MapRoute("default", "api/{controller}/{action}/{id?}")
            );
        }
    }
}
