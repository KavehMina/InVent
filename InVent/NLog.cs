using NLog;
using NLog.Web;

namespace InVent
{
    public static class NLogConfiguration
    {
        public static NLog.LogFactory ConfigureNLog()
        {
            NLog.LogFactory result = null;
            var ffname = Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]);
            //var name = configuration["name"] ?? configuration["applicationName"];
            var name = "portal-access-logs";
            name = string.IsNullOrWhiteSpace(name)
                ? Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0])
                : name;
            var folder = "portal-logs";
            var default_layout = "${longdate}|${uppercase:${level}}|${logger}::: ${message} ${exception:format=tostring}";
            string getFileName(string n)
            {
                return $"c:\\temp\\{folder}\\{name} {n} [${{shortdate}}].log";
            }
            if (1 == 1 || !File.Exists("nlog.config"))
            {
                var config = new NLog.Config.LoggingConfiguration();
                config.AddTarget(new NLog.Targets.ColoredConsoleTarget
                {
                    Name = "console",
                    Layout = "${logger} (${level:uppercase=true}):::${message}"
                });
                config.AddTarget(new NLog.Targets.FileTarget
                {
                    Name = "trace",
                    FileName = getFileName("Trace"),
                    Layout = default_layout
                });
                config.AddTarget(new NLog.Targets.FileTarget
                {
                    Name = "info",
                    FileName = getFileName("Info"),
                    Layout = default_layout
                });
                config.AddTarget(new NLog.Targets.FileTarget
                {
                    Name = "audit",
                    FileName = getFileName("audit"),
                    Layout = default_layout
                });
                config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, "trace");
                config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, "info");
                config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, "console");
                config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, "audit", "Audit*");
                //result = NLog.Web.NLogBuilder.ConfigureNLog(config);
                result = NLog.LogManager.Setup().LoadConfiguration(config).LogFactory;//?
                Console.WriteLine($"NLog Configured. FileName:'{getFileName("")}'");

                return result;
            }
            else
            {
                result = NLog.LogManager.Setup().LoadConfigurationFromAppSettings("nlog.config").LogFactory;//?
                //result = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");
                //result.Configuration.Variables.Add("TTT", new NLog.Layouts.SimpleLayout { Text = "lll" });

                return result;
            }

        }
    }
}
