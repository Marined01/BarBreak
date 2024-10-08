using Serilog;
using Serilog.Events;

namespace BarBreak.Infrastructure
{
    internal static class LoggingConfiguration
    {
        public static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() 
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) 
                .Enrich.FromLogContext()
                .WriteTo.Console() 
                .WriteTo.Seq("http://localhost:5341") 
                .CreateLogger();
        }
    }
}
