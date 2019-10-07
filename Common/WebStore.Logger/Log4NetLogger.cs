using System;
using System.Reflection;
using System.Xml;
using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WebStore.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _Log;

        public Log4NetLogger(string Category, XmlElement config)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _Log = LogManager.GetLogger(logger_repository.Name, Category);

            log4net.Config.XmlConfigurator.Configure(logger_repository, config);
        }

        public bool IsEnabled(LogLevel LogLevel)
        {
            switch (LogLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(LogLevel), LogLevel, null);

                case LogLevel.Trace: 
                case LogLevel.Debug:
                    return _Log.IsDebugEnabled;

                case LogLevel.Information:
                    return _Log.IsInfoEnabled;

                case LogLevel.Warning:
                    return _Log.IsWarnEnabled;

                case LogLevel.Error:
                    return _Log.IsErrorEnabled;

                case LogLevel.Critical:
                    return _Log.IsFatalEnabled;

                case LogLevel.None:
                    return false;
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public void Log<TState>(LogLevel LogLevel, EventId Id, TState State, Exception exception, Func<TState, Exception, string> formatter)
        {
            if(!IsEnabled(LogLevel)) return;
            
            if(formatter is null) throw new ArgumentNullException(nameof(formatter));

            var log_message = formatter(State, exception);

            if(string.IsNullOrEmpty(log_message) && exception is null) return;

            switch (LogLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(LogLevel), LogLevel, null);

                case LogLevel.Trace: 
                case LogLevel.Debug: 
                    _Log.Debug(log_message);
                    break;

                case LogLevel.Information: 
                    _Log.Info(log_message);
                    break;

                case LogLevel.Warning:
                    _Log.Warn(log_message);
                    break;

                case LogLevel.Error:
                    _Log.Error(log_message ?? exception.ToString());
                    break;

                case LogLevel.Critical: 
                    _Log.Fatal(log_message ?? exception.ToString());
                    break;

                case LogLevel.None:
                    break;
            }
        }

    }
}