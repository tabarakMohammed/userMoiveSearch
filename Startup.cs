using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.Configrations;
using user_moive_search.DataAcessLayer.Models;
using user_moive_search.middelware;
using user_moive_search.middelware.workers.ELK;

namespace user_moive_search
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
            var mongoDbSetting = Configuration.GetSection(nameof(MongoDbIdentityConfig)).Get<MongoDbIdentityConfig>();
          
            services.AddIdentity<Users, Role>().
                AddMongoDbStores<Users, Role, Guid>(
                mongoDbSetting.conictionString, mongoDbSetting.name);
          
            services.AddElasticsearch(Configuration);
            
           // services.AddControllersWithViews();
            services.AddRazorPages();


            services.AddScoped<IElkWorker, ElkWorker>();
            services.AddScoped<ElkService>().AddElasticsearch(Configuration);
           




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
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                /*
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=Home}/{Action=Index}/{id=?}");
                */
            });
        }
    }
}
