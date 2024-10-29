namespace BarBreak.Infrastructure;

using Serilog;
using Serilog.Events;

internal static class LoggingConfiguration
{
    public static void ConfigureLogging()
    {
        // it's better to move the level of logs to config files
        // you can use IConfigurationRoot or IConfiguration and do it better
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() 
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) 
            .Enrich.FromLogContext()
            .WriteTo.Console() 
            .WriteTo.Seq("http://localhost:5341") 
            .CreateLogger();
    }
}