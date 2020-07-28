using System;
using System.Collections.Generic;
using System.Text;

namespace Richard.Core.Setup
{
    public static class SwaggerSetup
    {
        public static void ConfigSetUp(this IServiceCollection service)
        {
            if (service == null)
            {
                throw new ArgumentException(nameof(service));
            }

            string basePath = AppContext.BaseDirectory;
        }
    }
}
