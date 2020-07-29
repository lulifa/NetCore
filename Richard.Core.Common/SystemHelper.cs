using System;
using System.Collections.Generic;
using System.Text;

namespace Richard.Core.Common
{
    public static class SystemHelper
    {
        public static readonly string[] SectionGroup = new string[] { "Startup", "ApiName" };
        public enum ApiVersions
        {
            V1 = 1,
            V2 = 2
        }
    }
}
