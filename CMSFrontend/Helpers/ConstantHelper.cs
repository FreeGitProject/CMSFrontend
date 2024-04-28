﻿

using System.Configuration;

namespace CMSFrontend.Helpers
{
    public class ConstantHelper
    {
        public const string CreateCollection = "api/v1/CMS/collection";

        
    }

    public partial class ConfigKeys {
        public static readonly string CMSAPIBaseUrl = ConfigurationManager.AppSettings.Get("CMSAPIBaseUrl");
    }
}