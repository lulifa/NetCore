using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Richard.Core.Common;
using static Richard.Core.Setup.CustomApiVersion;

namespace Richard.Core.Middleware
{
    public static class SwaggerMid
    {
        private static readonly string[] sectionGroup = new string[] { "Startup", "ApiName" };
        public static void Config(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c =>
            {
                string apiName = AppSettingHelper.GetSection(sectionGroup);
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{apiName} {version}");
                });
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
