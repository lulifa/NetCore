using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Richard.Core.Common
{
    public static class SystemHelper
    {
        public static readonly string JwtOauth2 = "oauth2";
        public static readonly string[] SectionGroup = new string[] { "Startup", "ApiName" };
        public static readonly string[] SecretGroup = new string[] { "Audience", "Secret" };
        public static readonly string[] SecretFileGroup = new string[] { "Audience", "SecretFile" };

        public static readonly string DBS = "DBS";
        public static readonly string MutiDBEnabled = "MutiDBEnabled";
        public static readonly string CQRSEnabled = "CQRSEnabled";
        public static readonly string MainDB = "MainDB";

        public static  string Audience_Secret => InitAudienceSecret();

        private static string InitAudienceSecret()
        {
            string secret = AppSettingHelper.GetSection(SecretGroup);
            string secretFile= AppSettingHelper.GetSection(SecretFileGroup);
            string secretKey = secret;
            if (!string.IsNullOrEmpty(secretFile))
            {
                if (File.Exists(secretFile))
                {
                    string readSecret= File.ReadAllText(secretFile);
                    if (!string.IsNullOrEmpty(readSecret))
                    {
                        secretKey = readSecret;
                    }
                }
            }
            return secretKey;
        }

        public enum ApiVersions
        {
            V1 = 1,
            V2 = 2
        }
    }
}
