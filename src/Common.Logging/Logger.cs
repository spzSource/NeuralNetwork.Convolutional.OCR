using System;
using System.Configuration;
using System.IO;

using log4net;
using log4net.Config;

namespace DigitR.Common.Logging
{
    /// <summary>
    /// The wrapper for specific logging framework.
    /// </summary>
    public class Logger
    {
        private readonly ILog logger;

        /// <summary>
        /// The .ctor for <see cref="Logger"/> class.
        /// </summary>
        public Logger()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            string configFilePath = configuration.FilePath;
            string loggerName = configuration.AppSettings.Settings["LoggerName"].Value;

            logger = LogManager.GetLogger(loggerName);
            
            XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));
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
