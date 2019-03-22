using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace Nadmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => { logging.ClearProviders(); })
                .UseNLog()
                .UseKestrel(option =>
                {
                    option.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                    option.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(2);
                })
                .UseStartup<Startup>();
    }
}
