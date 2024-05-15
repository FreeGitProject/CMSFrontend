

using System.Configuration;

namespace CMSFrontend.Helpers
{
    public class ConstantHelper
    {
        public const string CreateCollection = "api/v1/CMS/collection";
        public const string GetCollections = "api/v1/CMS/collections";

        
    }

    public partial class ConfigKeys {
        public static readonly string CMSAPIBaseUrl = ConfigurationManager.AppSettings.Get("CMSAPIBaseUrl");
        public static readonly string AuthorizationServerBaseAddress = ConfigurationManager.AppSettings.Get("AuthorizationServerBaseAddress");
        public static readonly string AuthClientSecret = ConfigurationManager.AppSettings.Get("AuthClientSecret");
        public static readonly string AuthClientId = ConfigurationManager.AppSettings.Get("AuthClientId");
    }
}