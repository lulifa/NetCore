using Microsoft.Extensions.DependencyInjection;
using Richard.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using static Richard.Core.Setup.CustomApiVersion;

namespace Richard.Core.Setup
{
    public static class SwaggerSetup
    {
        private static readonly string[] SectionGroup = new string[] {"Startup", "ApiName"};
        public static void Config(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentException(nameof(services));
            }

            string basePath = AppContext.BaseDirectory;
            string apiName = AppSettingHelper.GetSection(SectionGroup);
            services.AddSwaggerGen(c =>
            {
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
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
                            Url = new Uri("https://www.cnblogs.com/CoCoSpring/")
                        },
                        License = new OpenApiLicense
                        {
                            Name = $"{apiName} 官方文档",
                            Url = new Uri("https://www.cnblogs.com/CoCoSpring/")
                        }
                    });
                    c.OrderActionsBy(o => o.RelativePath);

                    try
                    {
                        string xmlPath = Path.Combine(basePath, "Richard.Core.Api.xml");
                        c.IncludeXmlComments(xmlPath, true);
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

    public class CustomApiVersion
    {
        public enum ApiVersions
        {
            V1=1,
            V2=2
        }
    }
}
