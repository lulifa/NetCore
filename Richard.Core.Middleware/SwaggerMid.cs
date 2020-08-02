using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Richard.Core.Common;
using System.Linq;

namespace Richard.Core.Middleware
{
    public static class SwaggerMid
    {
        public static void ConfigSwaggerMid(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c =>
            {
                string apiName = AppSettingHelper.GetSection(SystemHelper.SectionGroup);
                typeof(SystemHelper.ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{apiName} {version}");
                });
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
