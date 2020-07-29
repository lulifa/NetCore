using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Richard.Core.Common
{
    public static class AppSettingHelper
    {

        public static string GetSection(this IConfiguration configuration, params string[] sections)
        {
            string section = string.Empty;
            if (sections.Any())
            {
                section = configuration[string.Join(":", sections)];
            }
            return section;
        }

        public static List<T> GetSection<T>(this IConfiguration configuration,params string[] sections)
        {
            List<T> sectionGroups = new List<T>();
            if (sections.Any())
            {
                configuration.Bind(string.Join(":", sections), sectionGroups);
            }
            return sectionGroups;
        }
    }
}
