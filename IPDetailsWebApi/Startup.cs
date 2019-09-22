using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IPDetailsWebApi.Startup))]

namespace IPDetailsWebApi
{
    public class Startup
    {
        private IEnumerable<IDisposable> GetHangfireServers()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HangFireConnectionString"].ConnectionString;
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
               .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
               {
                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                   QueuePollInterval = TimeSpan.Zero,
                   UseRecommendedIsolationLevel = true,
                   UsePageLocksOnDequeue = true,
                   DisableGlobalLocks = true
                  
               });

            yield return new BackgroundJobServer();
        }
        public void Configuration(IAppBuilder app)
        {
            //HangFire configuration
            app.UseHangfireAspNet(GetHangfireServers);
            
        }
    }
}
