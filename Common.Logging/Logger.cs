using System;
using System.Configuration;
using System.IO;

using log4net;
using log4net.Config;

namespace DigitR.Common.Logging
{
    public class Logger
    {
        private readonly ILog logger;

        public Logger()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string configFilePath = configuration.FilePath;

            logger = LogManager.GetLogger("DigitR.Log");
            FileInfo configFile = new FileInfo(configFilePath);
            XmlConfigurator.ConfigureAndWatch(configFile);
        }

        public void Info(string message, params object[] objects)
        {
            logger.InfoFormat(message, objects);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Error(string message, params object[] objects)
        {
            logger.ErrorFormat(message, objects);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(Exception exception)
        {
            logger.Error(exception.Message, exception);
        }

        public void Error(string message, Exception ex)
        {
            logger.Error(message, ex);
        }
    }
}
