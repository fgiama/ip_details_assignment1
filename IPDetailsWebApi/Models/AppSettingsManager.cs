using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IPDetailsWebApi.Models
{
    public static class AppSettingsManager 
    {
        public static int? CacheExpirationTime
        {
            get {
                var cacheExpirationTimeSetting = ConfigurationManager.AppSettings["CachExiprationInMin"];
                if (cacheExpirationTimeSetting != null)
                    return Convert.ToInt32(cacheExpirationTimeSetting);
                return null;
            }
        }

        public static int BucketsToStore
        {
            get
            {
                var cacheExpirationTimeSetting = ConfigurationManager.AppSettings["BucketsToStore"];
                if (cacheExpirationTimeSetting != null)
                    return Convert.ToInt32(cacheExpirationTimeSetting);
                return 10;
            }
        }

    }
}