using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Richard.Core.Common;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Richard.Core.Setup
{
    public static class SwaggerSetup
    {
        
        public static void ConfigSwaggerSetup(this IServiceCollection services,IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            string basePath = AppContext.BaseDirectory;
            string apiName = configuration.GetSection(SystemHelper.SectionGroup);
            services.AddSwaggerGen(c =>
            {
                typeof(SystemHelper.ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = $"{apiName} 接口文档-{RuntimeInformation.FrameworkDescription}",
                        Description = $"{apiName} HTTP API " + version,
                        Contact = new OpenApiContact
                        {
                            Name = apiName,
                            Email = "814570123@qq.com",
                            Url = new Uri("https://www.cnblogs.com/lulifa/")
                        },
                        License = new OpenApiLicense
                        {
                            Name = $"{apiName} 官方文档",
                            Url = new Uri("https://www.cnblogs.com/lulifa/")
                        }
                    });
                    c.OrderActionsBy(o => o.RelativePath);

                    try
                    {
                        string xmlPath = Path.Combine(basePath, "Richard.Core.Api.xml");
                        c.IncludeXmlComments(xmlPath, true);
                        string xmlModelPath = Path.Combine(basePath, "Richard.Core.Entity.xml");//这个就是Model层的xml文件名
                        c.IncludeXmlComments(xmlModelPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                });
            });
        }
    }
}
