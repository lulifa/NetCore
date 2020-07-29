using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Richard.Core.Common
{
    public class AppSettingHelper
    {
        private static IConfiguration Configuration { get; set; }

        public AppSettingHelper(IConfiguration configuration)
        {
            Configuration = configuration;
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
