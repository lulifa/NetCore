using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Richard.Core.Common
{
    public class AppSettingHelper
    {
        private static IConfiguration Configuration { get; set; }
        private static string BasePath { get; set; }
        public AppSettingHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public AppSettingHelper(string basePath)
        {
            BasePath = basePath;
            Configuration = GetConfigurationByPath(BasePath);
        }

        private IConfiguration GetConfigurationByPath(string basePath)
        {
            IConfiguration configuration= new ConfigurationBuilder().SetBasePath(basePath).Add(new JsonConfigurationSource
            {
                Path = basePath,
                Optional = false,
                ReloadOnChange = true
            }).Build();
            return configuration;
        }

        public static string GetSection(params string[] sections)
        {
            string section = string.Empty;
            if (sections.Any())
            {
                section = Configuration[string.Join(":", sections)];
            }
            return section;
        }

        public static List<T> GetSection<T>(params string[] sections)
        {
            List<T> sectionGroups = new List<T>();
            if (sections.Any())
            {
                Configuration.Bind(string.Join(":", sections), sectionGroups);
            }
            return sectionGroups;
        }


    }
}
