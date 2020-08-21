using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Richard.Core.Common;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Linq;
using System.Reflection;
using System;
using System.IO;

namespace Richard.Core.Middleware
{
    public static class SwaggerMid
    {
        public static void ConfigSwaggerMid(this IApplicationBuilder application, Func<Stream> streamHtml)
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
                // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：{项目名.index.html}
                if (streamHtml.Invoke() == null)
                {
                    var msg = "index.html的属性，必须设置为嵌入的资源";
                    throw new Exception(msg);
                }
                c.IndexStream = streamHtml;
                c.DefaultModelsExpandDepth(-1); //设置为 - 1 可不显示models
                c.DocExpansion(DocExpansion.None); //设置为none可折叠所有方法
            });
        }
    }
}
