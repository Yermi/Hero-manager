using System.IO;
using System.Reflection;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace WebApi.Infrastructure
{
    public static class Logger
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static ILog Log
        {
            get { return _log; }
        }

        public const string CONFIG_PATH = "\\Infrastructure\\Logger\\log4net.config";

        public static void Setup()
        {
            ILoggerRepository repository = log4net.LogManager.GetRepository(Assembly.GetCallingAssembly());
            var configPath = Directory.GetCurrentDirectory() + CONFIG_PATH;
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo(configPath));
        }
    }
}