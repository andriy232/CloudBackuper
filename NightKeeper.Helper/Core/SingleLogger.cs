using Microsoft.Extensions.Configuration;
using Serilog;

namespace NightKeeper.Helper.Core
{
    public class SingleLogger
    {
        public void Init()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            CurrentLog = Serilog.Log.Logger;
        }

        public ILogger CurrentLog { get; set; }
    }
}