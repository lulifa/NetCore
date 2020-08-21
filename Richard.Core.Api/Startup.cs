using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Richard.Core.Common;
using Richard.Core.IRepository;
using Richard.Core.IService;
using Richard.Core.Middleware;
using Richard.Core.Repository;
using Richard.Core.Service;
using Richard.Core.Setup;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Richard.Core.Api
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
            
            services.AddControllers();
            services.AddSingleton(new AppSettingHelper(Configuration));
            //services.AddSingleton(new AppSettingHelper(ApplicationEnvironment.ApplicationBasePath));

            services.AddScoped<ICacheService, MemoryCacheService>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            services.ConfigSqlSugarSetup();
            
            services.ConfigSwaggerSetup();
            services.ConfigMiniProfilerSetup();
            services.ConfigureBearerJwtSetup();
            

            //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddScoped(typeof(IBaseServices<>), typeof(BaseServices<>));
            //services.AddScoped(typeof(IBlogServices), typeof(BlogServices));
            //services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoFacSetup());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.ConfigSwaggerMid(()=> GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Richard.Core.Api.index.html"));
            }
            app.UseHttpsRedirection();

            // 使用静态文件
            //app.UseStaticFiles();

            app.UseMiniProfiler();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
